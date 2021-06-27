using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKMapGenerator.Ultima;
using SKMapGenerator.Generation;
using System.IO;

namespace SKMapGenerator
{
    public partial class FormMain : Form
    {
        string _lastDir = Environment.CurrentDirectory;

        StringBuilder _outBuilder;

        short _defaultTileId;
        sbyte _defaultHeight;

        bool _useHeightMap;
        string _heightMapPath;
        string _heightMapCSPath;

        bool _useTileMap;
        string _tileMapPath;
        string _tileMapCSPath;

        int _mapIndex;
        string _outputDir;

        string _staticApplyArrayPath;
        bool _applyStatics;

        public FormMain()
        {
            InitializeComponent();

            _outBuilder = new StringBuilder();
            _lastDir = Environment.CurrentDirectory;

            _heightMapPath = TBHeightmap.Text;
            _heightMapCSPath = TBHeightmapCS.Text;
            _tileMapPath = TBTilemap.Text;
            _tileMapCSPath = TBTilemapCS.Text;
            _staticApplyArrayPath = TBStatics.Text;
            _useHeightMap = CBUseHeightmap.Checked;
            _useTileMap = CBUseTilemap.Checked;

            _outputDir = TBOutputDir.Text = Path.Combine(Environment.CurrentDirectory, TBOutputDir.Text);
            _mapIndex = (int)NUMMapIndex.Value;
        }

        void RefreshPaths()
        {
            _heightMapPath = TBHeightmap.Text;
            _heightMapCSPath = TBHeightmapCS.Text;
            _tileMapPath = TBTilemap.Text;
            _tileMapCSPath = TBTilemapCS.Text;
            _outputDir = TBOutputDir.Text;
            _staticApplyArrayPath = TBStatics.Text;
            _useHeightMap = CBUseHeightmap.Checked;
            _useTileMap = CBUseTilemap.Checked;
        }

        string SelectFileDialog(string extension, string defaultName, string title, Action onCancel = null)
        {
            openFileDialog1.Reset();
            openFileDialog1.Filter = $"{extension}|{extension}";
            openFileDialog1.InitialDirectory = _lastDir;
            openFileDialog1.FileName = defaultName;
            openFileDialog1.Title = title;
            openFileDialog1.ValidateNames = true;

            DialogResult result = openFileDialog1.ShowDialog();

            switch(result)
            {
                default:
                case DialogResult.Cancel:
                    onCancel?.Invoke();
                    return null;

                case DialogResult.OK:
                    return _lastDir = openFileDialog1.FileName;
            }

        }

        void ClearOut()
        {
            if (InvokeRequired)
                Invoke(new Action(Clear));
            else
                Clear();

            void Clear()
            {
                _outBuilder.Clear();
                RTBOutput.Text = string.Empty;
            }
        }

        void WriteLineOut(string line)
        {
            if (InvokeRequired)
                Invoke(new Action(WriteLine));
            else
                WriteLine();
            
            void WriteLine()
            {
                if (string.IsNullOrEmpty(line))
                    return;

                _outBuilder.AppendLine(line);
                RTBOutput.Text = _outBuilder.ToString();
            }
        }

        async void BTNGenerateMap_Click(object sender, EventArgs e)
        {
            RefreshPaths();
            ClearOut();

            if (!_useHeightMap && !_useTileMap)
            {
                WriteLineOut("You have to either select heightmap, tilemap or both");
                return;
            }
            else if ((_useHeightMap && (!CheckFilePath(_heightMapPath, "Heightmap") || !CheckFilePath(_heightMapCSPath, "Heightmap colorstore"))) ||
                     (_useTileMap && (!CheckFilePath(_tileMapPath, "Tilemap") || !CheckFilePath(_tileMapCSPath, "Tilemap colorstore"))) ||
                     (_applyStatics && !CheckFilePath(_staticApplyArrayPath, "Static apply array")))
                return;

            WriteLineOut("Generating map...");

            try
            {
                await Task.Run(() =>
                {
                    CheckOutputFolder();

                    TileMatrix tileMatrix = null;

                    if (_useHeightMap)
                    {
                        WriteLineOut("Loading heightmap");
                        using (Bitmap bmpHeights = (Bitmap)Image.FromFile(_heightMapPath))
                        {
                            tileMatrix = new TileMatrix(bmpHeights.Width, bmpHeights.Height, _defaultTileId, _defaultHeight);

                            ColorStore csHeights = new ColorStore(255);
                            csHeights.Load(_heightMapCSPath);

                            HeightMapGenerator hmg = new HeightMapGenerator();

                            WriteLineOut("Applying heightmap");
                            hmg.Generate(csHeights, bmpHeights, tileMatrix);
                            WriteLineOut("Applyed heightmap");
                        }
                    }

                    if (_useTileMap)
                    {
                        WriteLineOut("Loading tilemap");
                        using (Bitmap bmpTiles = (Bitmap)Image.FromFile(_tileMapPath))
                        {
                            if (!_useHeightMap)
                                tileMatrix = new TileMatrix(bmpTiles.Width, bmpTiles.Height, _defaultTileId, _defaultHeight);

                            ColorStore csTiles = new ColorStore(255);
                            csTiles.Load(_tileMapCSPath);

                            TileMapGenerator tmg = new TileMapGenerator();
                            WriteLineOut("Applying tilemap");
                            tmg.Generate(csTiles, bmpTiles, tileMatrix);
                            WriteLineOut("Applyed tilemap");
                        }
                    }

                    if (tileMatrix == null)
                    {
                        WriteLineOut("Error: TileMatrix null");
                        return;
                    }

                    Map map = new Map(tileMatrix.Width, tileMatrix.Height, _defaultTileId, _defaultHeight);
                    map.Tiles = tileMatrix;

                    if (_applyStatics)
                    {
                        StaticApplyArray saa = StaticApplyArray.Load(_staticApplyArrayPath);

                        if (saa == null)
                        {
                            WriteLineOut("Invalid statics apply array file");
                            return;
                        }

                        StaticGenerator sg = new StaticGenerator();

                        WriteLineOut("Applying statics");
                        sg.Generate(saa, map.StaticTiles);
                        WriteLineOut("Applyed statics");
                    }

                    WriteLineOut("Saving map");

                    map.SetPaths($"{_outputDir}\\map{_mapIndex}.mul",
                                $"{_outputDir}\\statics{_mapIndex}.mul",
                                $"{_outputDir}\\staidx{_mapIndex}.mul");
                    
                    map.Save();

                    WriteLineOut("Saved map\nDone generating map\n-----------------------------------");
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ClearOut();
                WriteLineOut($"Something went wrong while generating the map:\n\n{ex}");
            }
        }

        bool CheckFilePath(string file, string info)
        {
            if (!File.Exists(file))
            {
                WriteLineOut($"Unable to find file ({info}): {file}");
                return false;
            }

            return true;
        }

        void CheckOutputFolder()
        {
            if (!Directory.Exists(_outputDir))
            {
                Directory.CreateDirectory(_outputDir);
                return;
            }

            BackupFile($"{_outputDir}\\map{_mapIndex}.mul");
            BackupFile($"{_outputDir}\\statics{_mapIndex}.mul");
            BackupFile($"{_outputDir}\\staidx{_mapIndex}.mul");

            void BackupFile(string path)
            {
                if (File.Exists(path))
                {
                    if (File.Exists($"{path}.backup"))
                    {
                        if (File.Exists($"{path}.backup.backup"))
                            File.Delete($"{path}.backup");
                        else
                            File.Move($"{path}.backup", $"{path}.backup.backup");
                    }

                    File.Move(path, $"{path}.backup");
                }
            }
        }

        void BTNHeightmap_Click(object sender, EventArgs e)
        {
            _heightMapPath = TBHeightmap.Text = SelectFileDialog("*.bmp", "heightmap.bmp", "Select heightmap image");
        }

        void BTNHeightmapCS_Click(object sender, EventArgs e)
        {
            _heightMapCSPath = TBHeightmapCS.Text = SelectFileDialog("*.colorstore", "heightmap.colorstore", "Select heightmap colorstore");
        }

        void BTNTilemap_Click(object sender, EventArgs e)
        {
            _tileMapPath = TBTilemap.Text = SelectFileDialog("*.bmp", "tilemap.bmp", "Select tilemap image");
        }

        void BTNTilemapCS_Click(object sender, EventArgs e)
        {
            _tileMapCSPath = TBTilemapCS.Text = SelectFileDialog("*.colorstore", "tilemap.colorstore", "Select tilemapmap colorstore");
        }

        void BTNOutputDir_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            switch(result)
            {
                case DialogResult.OK:
                    break;

                default:
                    return;
            }

            _outputDir = TBOutputDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void CBUseHeightmap_CheckedChanged(object sender, EventArgs e)
        {
            _useHeightMap = CBUseHeightmap.Checked;
        }

        private void CBUseTilemap_CheckedChanged(object sender, EventArgs e)
        {
            _useTileMap = CBUseTilemap.Checked;
        }

        private void NUMMapIndex_ValueChanged(object sender, EventArgs e)
        {
            _mapIndex = (int)NUMMapIndex.Value;
        }

        private void BTNStatics_Click(object sender, EventArgs e)
        {
            _staticApplyArrayPath = TBStatics.Text = SelectFileDialog("*.saa", "statics.saa", "Select statics apply array");
        }

        private void CBStatics_CheckedChanged(object sender, EventArgs e)
        {
            _applyStatics = CBStatics.Checked;
        }

        private void BTHeightmapAcoFile_Click(object sender, EventArgs e)
        {
            ColorStore cs = new ColorStore(255);
            cs.Load(_heightMapCSPath);
            cs.SaveAsACO($"{_heightMapCSPath}.aco");

            WriteLineOut($"Saved heightmap colorstore to {_heightMapCSPath}.aco");
        }

        private void BTTilemapAcoFile_Click(object sender, EventArgs e)
        {
            ColorStore cs = new ColorStore(255);
            cs.Load(_tileMapPath);
            cs.SaveAsACO($"{_tileMapCSPath}.aco");

            WriteLineOut($"Saved tilemap colorstore to {_tileMapCSPath}.aco");
        }
    }
}
