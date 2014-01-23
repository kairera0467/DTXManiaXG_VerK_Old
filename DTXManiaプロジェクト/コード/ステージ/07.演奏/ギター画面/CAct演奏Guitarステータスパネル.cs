using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
        public override void On活性化()
        {

            #region [ 本体位置 ]

            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
            {
                this.n本体0X = 515;
                this.n本体0Y = 521;

                this.n本体1X = 337;
                this.n本体2X = 693;

                this.n本体Y = 211;

                this.nグラフSX = 135;
                this.nグラフNX = 313;
            }
            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
            {
                this.n本体0X = 381;
                this.n本体0Y = 318;

                this.n本体1X = 383;
                this.n本体2X = 647;

                this.n本体Y = 392;

                this.nグラフSX = 267;
                this.nグラフNX = 267;
            }

            if (!CDTXMania.DTX.bチップがある.Guitar || !CDTXMania.DTX.bチップがある.Bass)
            {
                if (CDTXMania.ConfigIni.bGraph.Guitar)
                    this.n本体1X = this.n本体1X + this.nグラフNX;

                if (CDTXMania.ConfigIni.bGraph.Bass)
                    this.n本体2X = this.n本体2X - this.nグラフNX;

                if (CDTXMania.ConfigIni.bGraph.Guitar || CDTXMania.ConfigIni.bGraph.Bass)
                    this.n本体0X = this.n本体0X + this.nグラフSX;
            }

            #endregion

            float fNameFontサイズ = 0;

            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                fNameFontサイズ = 26;
            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                fNameFontサイズ = 33;
            
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftNameFont = new Font("Arial", fNameFontサイズ, FontStyle.Bold, GraphicsUnit.Pixel);
            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 20f, FontStyle.Regular, GraphicsUnit.Pixel);

            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            this.nDifficulty = CDTXMania.nSongDifficulty;
            base.On活性化();
        }
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Score numbers_Guitar.png" ) );

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                    this.txPart = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Part.png"));
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                    this.txPart = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Part_XG.png"));

                this.b4font = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.b4font);
                graphics.PageUnit = GraphicsUnit.Pixel;

                this.strPlayerName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strCardName) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strGroupName) ? "" : CDTXMania.ConfigIni.strGroupName;

                //ギターベースの有無を判断する仕様を考えて、生成などは分けておく。

                #region[ ギターNamePlate ]
                    Graphics gNamePlate = Graphics.FromImage(this.b4font);

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                {
                    this.bNamePlate = new Bitmap(250, 266);
                    gNamePlate = Graphics.FromImage(this.bNamePlate);
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_nameplate_Guitar.png"));
                    gNamePlate.DrawImage(this.iNamePlate, 0, 0, 250, 266);
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty.png"));
                    Rectangle Rect = new Rectangle(0, 0 + (this.nDifficulty * 38), 234, 38);
                    gNamePlate.DrawImage(this.iDifficulty, 7, 91, Rect, GraphicsUnit.Pixel);
                    gNamePlate.DrawString(this.strPlayerName, this.ftNameFont, Brushes.White, (float)48f, (float)57f);
                    gNamePlate.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, 16f, 30f);
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    this.bNamePlate = new Bitmap(250, 297);
                    gNamePlate = Graphics.FromImage(this.bNamePlate);
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_nameplate_Guitar_XG.png"));
                    gNamePlate.DrawImage(this.iNamePlate, 0, 0, 250, 297);
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty_XG.png"));
                    Rectangle Rect = new Rectangle(0, 2 + (this.nDifficulty * 64), 234, 60);
                    gNamePlate.DrawImage(this.iDifficulty, 6, 50, Rect, GraphicsUnit.Pixel);
                    gNamePlate.DrawString(this.strPlayerName, this.ftNameFont, Brushes.White, (float)52f, (float)6f);
                }

                gNamePlate.Dispose();
                this.iNamePlate.Dispose();
                this.iDifficulty.Dispose();
                #endregion

                #region[ 曲名パネル ]
                Graphics gSongPanel = Graphics.FromImage(this.b4font);

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                {
                    this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel.png"));
                    this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                    this.bSongPanel = new Bitmap(250, 112);

                    string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                    if (!File.Exists(path))
                    {
                        this.iAlbum = Image.FromFile(CSkin.Path(@"Graphics\5_preimage default.png"));
                    }
                    else
                    {
                        this.iAlbum = Image.FromFile(path);
                    }

                    gSongPanel = Graphics.FromImage(this.bSongPanel);
                    gSongPanel.DrawImage(this.iSongPanel, 0, 0, 250, 112);
                    gSongPanel.DrawImage(this.iAlbum, new Rectangle(20, 10, 51, 51), new Rectangle(0, 0, this.iAlbum.Width, this.iAlbum.Height), GraphicsUnit.Pixel);
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel_Guitar_XG.png"));
                    this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                    this.bSongPanel = new Bitmap(518, 68);

                    string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                    if (!File.Exists(path))
                    {
                        this.iAlbum = Image.FromFile(CSkin.Path(@"Graphics\5_preimage default.png"));
                    }
                    else
                    {
                        this.iAlbum = Image.FromFile(path);
                    }

                    gSongPanel = Graphics.FromImage(this.bSongPanel);
                    gSongPanel.DrawImage(this.iSongPanel, 0, 0, 518, 68);
                    gSongPanel.DrawImage(this.iAlbum, new Rectangle(172, 2, 64, 64), new Rectangle(0, 0, this.iAlbum.Width, this.iAlbum.Height), GraphicsUnit.Pixel);
                }

                #region[ 曲名 ]
                this.nStrlengthbydot = (int)gSongPanel.MeasureString(this.strPanelString, this.ftDisplayFont).Width;

                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                {
                    if (this.nStrlengthbydot > 212)
                    {
                        gSongPanel.ScaleTransform(212f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append);
                        //graphics.DrawString( this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 68f );
                        gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 22f / (212f / (float)this.nStrlengthbydot), 73f);
                        gSongPanel.ResetTransform();
                    }
                    else
                    {
                        gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)18f, (float)73f);
                    }

                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    if (this.nStrlengthbydot > 250)
                    {
                        gSongPanel.ScaleTransform(250.0f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append);
                        //graphics.DrawString( this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 54f );
                        gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 250f / (250f / (float)this.nStrlengthbydot), 18f);
                        gSongPanel.ResetTransform();
                    }
                    else
                    {
                        gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)250f, (float)18f);
                    }
                }
                #endregion

                #endregion

                #region[ ギターレベル ] //未実装

                Graphics graphics2 = Graphics.FromImage(this.b4font);
                this.bDifficulty = new Bitmap(0x170, 0x103);
                graphics2.PageUnit = GraphicsUnit.Pixel;
                graphics2 = Graphics.FromImage(this.bDifficulty);

                string str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 10f);
                if (CDTXMania.DTX.LEVEL.Guitar > 100)
                {
                    str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 100f);
                }
                else
                {
                    str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 10.0f + (CDTXMania.DTX.LEVELDEC.Guitar != 0 ? CDTXMania.DTX.LEVELDEC.Guitar / 100.0f : 0));
                }

                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.LeftCymbal == false) &&
                    (CDTXMania.DTX.bチップがある.LP == false) &&
                    (CDTXMania.DTX.bチップがある.LBD == false) &&
                    (CDTXMania.DTX.bチップがある.FT == false) &&
                    (CDTXMania.DTX.bチップがある.Ride == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    str = string.Format("{0:00}", CDTXMania.DTX.LEVEL.Guitar);
                }


                int width = (int)graphics.MeasureString(this.stパネルマップ[this.nIndex].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                //数字の描画部分。その左側。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.LeftCymbal == false) &&
                    (CDTXMania.DTX.bチップがある.LP == false) &&
                    (CDTXMania.DTX.bチップがある.LBD == false) &&
                    (CDTXMania.DTX.bチップがある.FT == false) &&
                    (CDTXMania.DTX.bチップがある.Ride == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                        graphics2.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 18f + width, 164f);
                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                        graphics2.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 24f + width, 158f);
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                        graphics2.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 12.0f + width, 164f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                        graphics2.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 14.0f + width, 134f);
                }
                width += (int)graphics2.MeasureString(str.Substring(0, 1), this.ftDifficultyL).Width;

                //数字の右。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.LeftCymbal == false) &&
                    (CDTXMania.DTX.bチップがある.LP == false) &&
                    (CDTXMania.DTX.bチップがある.LBD == false) &&
                    (CDTXMania.DTX.bチップがある.FT == false) &&
                    (CDTXMania.DTX.bチップがある.Ride == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                        graphics2.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, width, 176f);

                    else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                        graphics2.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 2f + width, 170f);
                }

                graphics2.Dispose();

                #endregion

                this.txDummy = new CTexture(CDTXMania.app.Device, this.bDifficulty, CDTXMania.TextureFormat, false);
                //this.txパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) );
                this.txパネル = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );
                //this.tx曲名パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_songpanel.png"));
                this.tx曲名パネル = new CTexture(CDTXMania.app.Device, this.bSongPanel, CDTXMania.TextureFormat, false);

                gSongPanel.Dispose();
                this.iSongPanel.Dispose();
                this.iAlbum.Dispose();

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );
                CDTXMania.tテクスチャの解放( ref this.txScore );
                CDTXMania.tテクスチャの解放( ref this.txDummy );
                CDTXMania.tテクスチャの解放( ref this.txPart );

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();
                this.ftLevelFont.Dispose();
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{

                this.tx曲名パネル.t2D描画(CDTXMania.app.Device, this.n本体0X, this.n本体0Y);

                //CDTXMania.act文字コンソール.tPrint(0, 100, C文字コンソール.Eフォント種別.白, string.Format("{0:####0}", CDTXMania.stage演奏ギター画面.bブーストボーナス ? 1 : 0));
				if( this.txパネル != null && CDTXMania.DTX.bチップがある.Guitar )
				{
			        this.txパネル.t2D描画( CDTXMania.app.Device, this.n本体1X, this.n本体Y );

                    if (this.txPart != null)
                    {
                        if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                            this.txPart.t2D描画(CDTXMania.app.Device, 7 + this.n本体1X, 91 + this.n本体Y, new Rectangle(0, (CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 76 : 38, 234, 38));
                        else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                            this.txPart.t2D描画(CDTXMania.app.Device, 6 + this.n本体1X, 50 + this.n本体Y, new Rectangle(0, 2 + ((CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 128 : 64), 234, 60));
                    }

//                    this.txDummy.t2D描画(CDTXMania.app.Device, this.n本体1X, this.n本体Y);

                int guitar = CDTXMania.ConfigIni.n譜面スクロール速度.Guitar;
                    #region[ スコア表示 ]
                    this.n現在のスコアGuitar = (long)CDTXMania.stage演奏ギター画面.actScore.n現在表示中のスコア.Guitar;
                    if( CDTXMania.ConfigIni.nSkillMode == 0 )
                    {
                        string str = this.n現在のスコアGuitar.ToString("0000000000");
                        for (int i = 0; i < 10; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;

                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体1X) / 0.7f) + (i * 25), 360 - (185 + 12 + this.n本体Y), 0);
                                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体1X) / 0.7f) + (i * 25), 360 - (170 + 12 + this.n本体Y), 0);

                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);

                            }
                        }
                    }
                    else if( CDTXMania.ConfigIni.nSkillMode == 1 )
                    {
                        string str = this.n現在のスコアGuitar.ToString("0000000");
                        for( int i = 0; i < 7; i++ )
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if( this.txScore != null )
                            {
                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 185 + this.n本体Y, rectangle);
                                else if( CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B )
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体1X + (i * 25), 170 + this.n本体Y, rectangle);
                           }
                        }
                    }
                    #endregion
				}

                if (this.txパネル != null && CDTXMania.DTX.bチップがある.Bass)
                {
                    this.txパネル.t2D描画(CDTXMania.app.Device, this.n本体2X, this.n本体Y);

                    if (this.txPart != null)
                    {
                        if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                            this.txPart.t2D描画(CDTXMania.app.Device, 7 + this.n本体2X, 91 + this.n本体Y, new Rectangle(0, (CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 38 : 76, 234, 38));
                        else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                            this.txPart.t2D描画(CDTXMania.app.Device, 6 + this.n本体2X, 50 + this.n本体Y, new Rectangle(0, 2 + ((CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 64 : 128), 234, 60));
                    }

                    int bass = CDTXMania.ConfigIni.n譜面スクロール速度.Bass;
                    #region[ スコア表示 ]
                    this.n現在のスコアBass = (long)CDTXMania.stage演奏ギター画面.actScore.n現在表示中のスコア.Bass;
                    if (CDTXMania.ConfigIni.nSkillMode == 0)
                    {
                        string str = this.n現在のスコアBass.ToString("0000000000");
                        for (int i = 0; i < 10; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;

                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体2X) / 0.7f) + (i * 25), 360 - (185 + 12 + this.n本体Y), 0);
                                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体2X) / 0.7f) + (i * 25), 360 - (170 + 12 + this.n本体Y), 0);

                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                                this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);

                            }
                        }
                    }
                    else if (CDTXMania.ConfigIni.nSkillMode == 1)
                    {
                        string str = this.n現在のスコアBass.ToString("0000000");
                        for (int i = 0; i < 7; i++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 24, 25);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                if (num3 < 5)
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                                else
                                {
                                    rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                                }
                            }
                            if (this.txScore != null)
                            {
                                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体2X + (i * 25), 185 + this.n本体Y, rectangle);
                                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体2X + (i * 25), 170 + this.n本体Y, rectangle);
                            }
                        }
                    }
                    #endregion
                }

			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//[StructLayout( LayoutKind.Sequential )]
		//private struct STATUSPANEL
		//{
		//    public string label;
		//    public int status;
		//}

		//private STATUSPANEL[] stパネルマップ;
        private int n本体0X;
        private int n本体0Y;
        private int n本体1X;
        private int n本体2X;
        private int n本体Y;
        private int nグラフSX;
        private int nグラフNX;
        private int nStrlengthbydot;
        private int nDifficulty;
        private CTexture txパネル;
        private CTexture tx曲名パネル;
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Bitmap bSongPanel;
        private Bitmap bDifficulty;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftNameFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private Image iAlbum;
        //private Image iDrumspeed;
        //private Image iRisky;
        private Image iNamePlate;
        private Image iSongPanel;
        private Image iDifficulty;
        private CTexture txScore;
        private CTexture txDummy;
        private CTexture txPart;
        //-----------------
		#endregion
	}
}
