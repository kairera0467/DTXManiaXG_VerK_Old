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

            if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
            {
                this.n本体X[0] = 515;
                this.n本体NY = 521;

                this.n本体X[1] = 337;
                this.n本体X[2] = 693;

                this.n本体SY = 211;

                this.nグラフSX = 135;
                this.nグラフNX = 313;
            }
            else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
            {
                this.n本体X[0] = 381;
                this.n本体NY = 318;

                this.n本体X[1] = 383;
                this.n本体X[2] = 647;

                this.n本体SY = 392;

                this.nグラフSX = 267;
                this.nグラフNX = 267;
            }

            if (CDTXMania.ConfigIni.bGraph有効)
            {
                if (!CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = this.n本体X[2] - this.nグラフNX;
                        this.n本体X[0] = this.n本体X[0] - this.nグラフSX;
                    }
                    else
                    {
                        this.n本体X[1] = this.n本体X[1] + this.nグラフNX;
                        this.n本体X[0] = this.n本体X[0] + this.nグラフSX;
                    }
                }
                else if (!CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = this.n本体X[1] + this.nグラフNX;
                        this.n本体X[0] = this.n本体X[0] + this.nグラフSX;
                    }
                    else
                    {
                        this.n本体X[2] = this.n本体X[2] - this.nグラフNX;
                        this.n本体X[0] = this.n本体X[0] - this.nグラフSX;
                    }
                }
                else if (!CDTXMania.ConfigIni.bギターが全部オートプレイである && CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {

                    this.n本体X[1] = this.n本体X[1] + this.nグラフNX;
                    this.n本体X[2] = 0;
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        this.n本体X[0] = this.n本体X[0] + this.nグラフSX;

                }
                else if (CDTXMania.ConfigIni.bギターが全部オートプレイである && !CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {
                    this.n本体X[2] = this.n本体X[2] - this.nグラフNX;
                    this.n本体X[1] = 0;
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        this.n本体X[0] = this.n本体X[0] - this.nグラフSX;
                }

            }

            #endregion

            this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 24, FontStyle.Bold ); //2013.09.07.kairera0467 PrivateFontへの移行テスト。
            this.ftGroupFont = new Font( "ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftDisplayFont = new Font( "ＤＦＧ平成ゴシック体W5", 20f, FontStyle.Regular, GraphicsUnit.Pixel );

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
                this.txSpeed = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_panel_icons.jpg" ) );
                this.txRisky = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_panel_icons2.jpg" ) );

                if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                    this.txPart = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Part.png" ) );
                else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                    this.txPart = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Part_XG.png" ) );

                this.b4font = new Bitmap(1, 1);

                this.strPlayerName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName ) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName ) ? "" : CDTXMania.ConfigIni.strGroupName;

                Image imgCustomSongNameTexture = Image.FromFile(CSkin.Path(@"Graphics\7_Dummy.png"));
                if( File.Exists( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" ) )
                {
                    imgCustomSongNameTexture = Image.FromFile( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" );
                }

                //ギターベースの有無を判断する仕様を考えて、生成などは分けておく。

                #region[ ネームカラー ]
                //--------------------
                Color clNameColor = Color.White;
                Color clNameColorLower = Color.White;
                switch (CDTXMania.ConfigIni.nNameColor)
                {
                    case 0:
                        clNameColor = Color.White;
                        break;
                    case 1:
                        clNameColor = Color.LightYellow;
                        break;
                    case 2:
                        clNameColor = Color.Yellow;
                        break;
                    case 3:
                        clNameColor = Color.Green;
                        break;
                    case 4:
                        clNameColor = Color.Blue;
                        break;
                    case 5:
                        clNameColor = Color.Purple;
                        break;
                    case 6:
                        clNameColor = Color.Red;
                        break;
                    case 7:
                        clNameColor = Color.Brown;
                        break;
                    case 8:
                        clNameColor = Color.Silver;
                        break;
                    case 9:
                        clNameColor = Color.Gold;
                        break;

                    case 10:
                        clNameColor = Color.White;
                        break;
                    case 11:
                        clNameColor = Color.LightYellow;
                        clNameColorLower = Color.White;
                        break;
                    case 12:
                        clNameColor = Color.Yellow;
                        clNameColorLower = Color.White;
                        break;
                    case 13:
                        clNameColor = Color.FromArgb(0, 255, 33);
                        clNameColorLower = Color.White;
                        break;
                    case 14:
                        clNameColor = Color.FromArgb(0, 38, 255);
                        clNameColorLower = Color.White;
                        break;
                    case 15:
                        clNameColor = Color.FromArgb(72, 0, 255);
                        clNameColorLower = Color.White;
                        break;
                    case 16:
                        clNameColor = Color.FromArgb(255, 255, 0, 0);
                        clNameColorLower = Color.White;
                        break;
                    case 17:
                        clNameColor = Color.FromArgb(255, 232, 182, 149);
                        clNameColorLower = Color.FromArgb(255, 122, 69, 26);
                        break;
                    case 18:
                        clNameColor = Color.FromArgb(246, 245, 255);
                        clNameColorLower = Color.FromArgb(125, 128, 137);
                        break;
                    case 19:
                        clNameColor = Color.FromArgb(255, 238, 196, 85);
                        clNameColorLower = Color.FromArgb(255, 255, 241, 200);
                        break;
                }

                Bitmap bmpCardName = new Bitmap(1, 1);

                if (CDTXMania.ConfigIni.nNameColor >= 11)
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent, clNameColor, clNameColorLower);
                }
                else
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent);
                }
                //--------------------
                #endregion

                #region[ NamePlate ]
                Graphics gNamePlate = Graphics.FromImage(this.b4font);

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    this.bNamePlate = new Bitmap(250, 266);
                    gNamePlate = Graphics.FromImage(this.bNamePlate);
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_nameplate_Guitar.png"));
                    gNamePlate.DrawImage(this.iNamePlate, 0, 0, 250, 266);
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty.png"));
                    Rectangle Rect1 = new Rectangle(7, 91, 234, 38);
                    Rectangle Rect2 = new Rectangle(0, 0 + (this.nDifficulty * 38), 234, 38);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                    gNamePlate.DrawImage(bmpCardName, 44f, 46f);
                    gNamePlate.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, 16f, 30f);
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    this.bNamePlate = new Bitmap(250, 297);
                    gNamePlate = Graphics.FromImage(this.bNamePlate);
                    this.iNamePlate = Image.FromFile(CSkin.Path(@"Graphics\7_nameplate_Guitar_XG.png"));
                    gNamePlate.DrawImage(this.iNamePlate, 0, 0, 250, 297);
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty_XG.png"));
                    Rectangle Rect1 = new Rectangle(6, 50, 234, 60);
                    Rectangle Rect2 = new Rectangle(0, 0 + (this.nDifficulty * 60), 234, 60);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                    gNamePlate.DrawImage(bmpCardName, 45f, 0f);
                }

                this.iNamePlate.Dispose();
                this.iDifficulty.Dispose();
                #endregion

                #region[ 曲名パネル ]
                Graphics gSongPanel = Graphics.FromImage(this.b4font);

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strPanelString = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;

                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.iAlbum = Image.FromFile(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.iAlbum = Image.FromFile(path);
                }

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    if( File.Exists( CSkin.Path(@"Graphics\7_songpanel.png") ) )
                    {
                        this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel.png"));
                        this.bSongPanel = new Bitmap(250, 112);

                        gSongPanel = Graphics.FromImage(this.bSongPanel);
                        gSongPanel.DrawImage(this.iSongPanel, 0, 0, 250, 112);
                        gSongPanel.DrawImage(this.iAlbum, new Rectangle(20, 10, 51, 51), new Rectangle(0, 0, this.iAlbum.Width, this.iAlbum.Height), GraphicsUnit.Pixel);
                    }
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel_Guitar_XG.png"));
                    this.bSongPanel = new Bitmap(518, 68);

                    gSongPanel = Graphics.FromImage(this.bSongPanel);
                    gSongPanel.DrawImage(this.iSongPanel, 0, 0, 518, 68);
                    gSongPanel.DrawImage(this.iAlbum, new Rectangle(172, 2, 64, 64), new Rectangle(0, 0, this.iAlbum.Width, this.iAlbum.Height), GraphicsUnit.Pixel);
                }

                #region[ 曲名 ]
                this.nStrlengthbydot = (int)gSongPanel.MeasureString(this.strPanelString, this.ftDisplayFont).Width;

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    if( File.Exists(CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png") )
                    {
                        gSongPanel.DrawImage(imgCustomSongNameTexture, 16, 70, 238, 30);
                    }
                    else
                    {
                        if (this.nStrlengthbydot > 212)
                        {
                            gSongPanel.ScaleTransform(212f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append);
                            gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 22f / (212f / (float)this.nStrlengthbydot), 73f);
                            gSongPanel.ResetTransform();
                        }
                        else
                        {
                            gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)18f, (float)73f);
                        }   
                    }

                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    if( File.Exists(CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png") )
                    {
                        gSongPanel.DrawImage(imgCustomSongNameTexture, 250, 18, 238, 30);
                    }
                    else
                    {
                        if (this.nStrlengthbydot > 250)
                        {
                            gSongPanel.ScaleTransform(250.0f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append);
                            gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 250f / (250f / (float)this.nStrlengthbydot), 18f);
                            gSongPanel.ResetTransform();
                        }
                        else
                        {
                            gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)250f, (float)18f);
                        }
                    }
                }
                #endregion

                #endregion

                #region[ ギターレベル ]

                Graphics gLevelG = Graphics.FromImage(this.b4font);

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                    this.bLevelG = new Bitmap(250, 266);
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                    this.bLevelG = new Bitmap(250, 297);

                gLevelG.PageUnit = GraphicsUnit.Pixel;
                gLevelG = Graphics.FromImage(this.bLevelG);

                string strG = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 10f);
                if (CDTXMania.DTX.LEVEL.Guitar > 100)
                {
                    strG = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 100f);
                }
                else
                {
                    strG = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Guitar) / 10.0f + (CDTXMania.DTX.LEVELDEC.Guitar != 0 ? CDTXMania.DTX.LEVELDEC.Guitar / 100.0f : 0));
                }

                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPGuitar == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    strG = string.Format("{0:00}", CDTXMania.DTX.LEVEL.Guitar);
                }

                int widthG = (int)gNamePlate.MeasureString(this.stパネルマップ[this.nIndex].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                //数字の描画部分。その左側。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPGuitar == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelG.DrawString(strG.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 91f + widthG, 89f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelG.DrawString(strG.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 44f + widthG, 66f);
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelG.DrawString(strG.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 91f + widthG, 89f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelG.DrawString(strG.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 44f + widthG, 66f);
                }
                widthG += (int)gNamePlate.MeasureString(strG.Substring(0, 1), this.ftDifficultyL).Width;

                //数字の右。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPGuitar == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelG.DrawString(strG.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 88f + widthG, 100f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelG.DrawString(strG.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 44f + widthG, 78f);
                }

                gLevelG.Dispose();

                #endregion

                #region[ ベースレベル ]

                Graphics gLevelB = Graphics.FromImage(this.b4font);

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                    this.bLevelB = new Bitmap(250, 266);
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                    this.bLevelB = new Bitmap(250, 297);

                gLevelB.PageUnit = GraphicsUnit.Pixel;
                gLevelB = Graphics.FromImage(this.bLevelB);

                string strB = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Bass) / 10f);
                if (CDTXMania.DTX.LEVEL.Bass > 100)
                {
                    strB = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Bass) / 100f);
                }
                else
                {
                    strB = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Bass) / 10.0f + (CDTXMania.DTX.LEVELDEC.Bass != 0 ? CDTXMania.DTX.LEVELDEC.Bass / 100.0f : 0));
                }

                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPBass == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    strB = string.Format("{0:00}", CDTXMania.DTX.LEVEL.Bass);
                }

                int widthB = (int)gNamePlate.MeasureString(this.stパネルマップ[this.nIndex].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                //数字の描画部分。その左側。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPBass == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelB.DrawString(strB.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 91f + widthB, 89f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelB.DrawString(strB.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 44f + widthB, 66f);
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelB.DrawString(strB.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 91f + widthB, 89f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelB.DrawString(strB.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 44f + widthB, 66f);
                }
                widthB += (int)gNamePlate.MeasureString(strB.Substring(0, 1), this.ftDifficultyL).Width;

                //数字の右。
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                    (CDTXMania.DTX.bチップがある.YPBass == false) &&
                    (CDTXMania.DTX.b強制的にXG譜面にする == false))
                {
                }
                else
                {
                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        gLevelB.DrawString(strB.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 88f + widthB, 100f);
                    else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        gLevelB.DrawString(strB.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 44f + widthB, 78f);
                }

                gLevelB.Dispose();

                #endregion

                this.txLevelG = new CTexture( CDTXMania.app.Device, this.bLevelG, CDTXMania.TextureFormat, false );
                this.txLevelB = new CTexture( CDTXMania.app.Device, this.bLevelB, CDTXMania.TextureFormat, false );
                //this.txパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) );
                this.txパネル = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );
                //this.tx曲名パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_songpanel.png"));
                this.tx曲名パネル = new CTexture( CDTXMania.app.Device, this.bSongPanel, CDTXMania.TextureFormat, false );

                gNamePlate.Dispose();
                gSongPanel.Dispose();
                this.b4font.Dispose();
                this.iSongPanel.Dispose();
                this.iAlbum.Dispose();
                this.bNamePlate.Dispose();
                this.bSongPanel.Dispose();
                this.bLevelG.Dispose();
                this.bLevelB.Dispose();

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
                CDTXMania.tテクスチャの解放( ref this.txSpeed );
                CDTXMania.tテクスチャの解放( ref this.txRisky );
                CDTXMania.tテクスチャの解放( ref this.txLevelG );
                CDTXMania.tテクスチャの解放( ref this.txLevelB );
                CDTXMania.tテクスチャの解放( ref this.txPart );

                this.ftDifficultyL.Dispose();
                this.ftDifficultyS.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftGroupFont.Dispose();
                this.ftLevelFont.Dispose();
                this.pfNameFont.Dispose();
                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if ( CDTXMania.ConfigIni.bShowMusicInfo )
                    this.tx曲名パネル.t2D描画(CDTXMania.app.Device, this.n本体X[0], this.n本体NY);

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    this.nSpeedX = 92;
                    this.nSpeedY = 136;
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    this.nSpeedX = 93;
                    this.nSpeedY = 123;
                }

                this.txSpeed.vc拡大縮小倍率 = new SlimDX.Vector3(32.0f / 42.0f, 32.0f / 48.0f, 1.0f);
                this.txRisky.vc拡大縮小倍率 = new SlimDX.Vector3(32.0f / 42.0f, 32.0f / 48.0f, 1.0f);

                //CDTXMania.act文字コンソール.tPrint(0, 100, C文字コンソール.Eフォント種別.白, string.Format("{0:####0}", CDTXMania.stage演奏ギター画面.bブーストボーナス ? 1 : 0));
                if ( this.txパネル != null && CDTXMania.DTX.bチップがある.Guitar && this.n本体X[1] != 0 && CDTXMania.ConfigIni.bShowScore )
				{
			        this.txパネル.t2D描画( CDTXMania.app.Device, this.n本体X[1], this.n本体SY );

                    if (this.txPart != null)
                    {
                        if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                            this.txPart.t2D描画(CDTXMania.app.Device, 7 + this.n本体X[1], 91 + this.n本体SY, new Rectangle(0, (CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 76 : 38, 234, 38));
                        else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                            this.txPart.t2D描画(CDTXMania.app.Device, 6 + this.n本体X[1], 50 + this.n本体SY, new Rectangle(0, 0 + ((CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 120 : 60), 234, 60));
                    }

                    this.nCurrentGuitarSpeed = CDTXMania.ConfigIni.n譜面スクロール速度.Guitar;

                    if (this.txSpeed != null)
                        this.txSpeed.t2D描画(CDTXMania.app.Device, this.nSpeedX + this.n本体X[1], this.nSpeedY + this.n本体SY, new Rectangle(0, ((this.nCurrentGuitarSpeed > 15) ? 15 : this.nCurrentGuitarSpeed) * 0x30, 0x2a, 0x30));

                    if (this.txRisky != null)
                        this.txRisky.t2D描画(CDTXMania.app.Device, 36 + this.nSpeedX + this.n本体X[1], this.nSpeedY + this.n本体SY, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));

                    if (!CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                        this.txLevelG.t2D描画(CDTXMania.app.Device, this.n本体X[1], this.n本体SY);
                    else
                        this.txLevelB.t2D描画(CDTXMania.app.Device, this.n本体X[1], this.n本体SY);

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

                                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体X[1]) / 0.7f) + (i * 25), 360 - (185 + 12 + this.n本体SY), 0);
                                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体X[1]) / 0.7f) + (i * 25), 360 - (170 + 12 + this.n本体SY), 0);

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
                                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体X[1] + (i * 25), 185 + this.n本体SY, rectangle);
                                else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体X[1] + (i * 25), 170 + this.n本体SY, rectangle);
                           }
                        }
                    }
                    #endregion
				}

                if ( this.txパネル != null && CDTXMania.DTX.bチップがある.Bass && this.n本体X[2] != 0 && CDTXMania.ConfigIni.bShowScore )
                {
                    this.txパネル.t2D描画(CDTXMania.app.Device, this.n本体X[2], this.n本体SY);

                    if (this.txPart != null)
                    {
                        if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                            this.txPart.t2D描画(CDTXMania.app.Device, 7 + this.n本体X[2], 91 + this.n本体SY, new Rectangle(0, (CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 38 : 76, 234, 38));
                        else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                            this.txPart.t2D描画(CDTXMania.app.Device, 6 + this.n本体X[2], 50 + this.n本体SY, new Rectangle(0, 0 + ((CDTXMania.ConfigIni.bIsSwappedGuitarBass) ? 60 : 120), 234, 60));
                    }

                    this.nCurrentBassSpeed = CDTXMania.ConfigIni.n譜面スクロール速度.Bass;

                    if (this.txSpeed != null)
                        this.txSpeed.t2D描画(CDTXMania.app.Device, this.nSpeedX + this.n本体X[2], this.nSpeedY + this.n本体SY, new Rectangle(0, ((this.nCurrentBassSpeed > 15) ? 15 : this.nCurrentBassSpeed) * 0x30, 0x2a, 0x30));

                    if (this.txRisky != null)
                        this.txRisky.t2D描画(CDTXMania.app.Device, 36 + this.nSpeedX + this.n本体X[2], this.nSpeedY + this.n本体SY, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));

                    if (!CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                        this.txLevelB.t2D描画(CDTXMania.app.Device, this.n本体X[2], this.n本体SY);
                    else
                        this.txLevelG.t2D描画(CDTXMania.app.Device, this.n本体X[2], this.n本体SY);

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

                                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体X[2]) / 0.7f) + (i * 25), 360 - (185 + 12 + this.n本体SY), 0);
                                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                                    matScoreXG *= SlimDX.Matrix.Translation(((-640 + 65 + 8 + this.n本体X[2]) / 0.7f) + (i * 25), 360 - (170 + 12 + this.n本体SY), 0);

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
                                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体X[2] + (i * 25), 185 + this.n本体SY, rectangle);
                                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                                    this.txScore.t2D描画(CDTXMania.app.Device, 65 + this.n本体X[2] + (i * 25), 170 + this.n本体SY, rectangle);
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
        private STDGBVALUE<int> n本体X;
        private int n本体SY;
        private int n本体NY;
        private int nグラフSX;
        private int nグラフNX;
        private int nSpeedX;
        private int nSpeedY;
        private int nCurrentGuitarSpeed;
        private int nCurrentBassSpeed;
        private int nStrlengthbydot;
        private int nDifficulty;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iAlbum;
        private Image iNamePlate;
        private Image iSongPanel;
        private Image iDifficulty;
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Bitmap bSongPanel;
        private Bitmap bLevelG;
        private Bitmap bLevelB;
        private CTexture txパネル;
        private CTexture tx曲名パネル;
        private CTexture txScore;
        private CTexture txSpeed;
        private CTexture txRisky;
        private CTexture txLevelG;
        private CTexture txLevelB;
        private CTexture txPart;
        private CPrivateFastFont pfNameFont;
        //-----------------
		#endregion
	}
}
