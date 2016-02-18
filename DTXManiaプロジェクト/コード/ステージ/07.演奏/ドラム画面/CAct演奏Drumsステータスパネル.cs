using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        public override void On活性化()
        {
            this.ftDisplayFont = new Font( "ＤＦＧ平成ゴシック体W5" , 22f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftGroupFont = new Font( "ＤＦＧ平成ゴシック体W5" , 16f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftLevelFont = new Font( "Impact", 26f, FontStyle.Regular );
            this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
            this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );

            this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 20, FontStyle.Bold ); //2013.09.07.kairera0467 PrivateFontへの移行テスト。
            this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 22, FontStyle.Regular );
            if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
            {
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                    ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) && 
                    ( CDTXMania.DTX.bチップがある.LP == false ) && 
                    ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                    ( CDTXMania.DTX.bチップがある.FT == false ) && 
                    ( CDTXMania.DTX.bチップがある.Ride == false ) &&
                    ( CDTXMania.DTX.b強制的にXG譜面にする == false) )
                {
                    this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
                    this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
                }
                else
                {
                    this.ftDifficultyL = new Font( "Arial", 48f, FontStyle.Bold );
                    this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
                }
            }
            this.nDifficulty = CDTXMania.nSongDifficulty;
            CDTXMania.strSongDifficulyName = this.stパネルマップ[ this.nDifficulty ].label;
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbers.png" ) );
                this.iPart = Image.FromFile(CSkin.Path(@"Graphics\7_Dummy.png"));
                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty_XG.png"));
                    this.iPart = Image.FromFile(CSkin.Path(@"Graphics\7_Part_XG.png"));
                }
                else
                {
                    this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Difficulty.png"));
                }
                #region[ ネームプレート本体 ]
                this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_Dummy.png" ) );
                if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                {
                    this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate.png" ) );
                    if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                        ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) && 
                        ( CDTXMania.DTX.bチップがある.LP == false ) && 
                        ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                        ( CDTXMania.DTX.bチップがある.FT == false ) && 
                        ( CDTXMania.DTX.bチップがある.Ride == false ) )
                    {
                        if( this.iNamePlate != null )
                            this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_cls.png" ) );
                    }
                }
                else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                {
                    this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_XG.png" ) );
                    if(CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                        ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) &&
                        ( CDTXMania.DTX.bチップがある.LP == false ) && 
                        ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                        ( CDTXMania.DTX.bチップがある.FT == false ) && 
                        ( CDTXMania.DTX.bチップがある.Ride == false ) )
                    {
                        if( this.iNamePlate != null )
                            this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_XG_cls.png" ) );
                    }
                }
                #endregion
                this.iDrumspeed = Image.FromFile( CSkin.Path( @"Graphics\7_panel_icons.jpg" ) );
                this.iRisky = Image.FromFile( CSkin.Path( @"Graphics\7_panel_icons2.jpg" ) );
                this.b4font = new Bitmap( 1, 1 );
                Graphics gNamePlate = Graphics.FromImage( this.b4font );
                gNamePlate.PageUnit = GraphicsUnit.Pixel;

                //Trace.TraceInformation("a");

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strPanelString = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;

                this.strPlayerName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[0] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[0];
                this.strGroupName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[0] ) ? "" : CDTXMania.ConfigIni.strGroupName[0];
                this.nStrlengthbydot = (int)gNamePlate.MeasureString(this.strPanelString, this.ftDisplayFont).Width;
                gNamePlate.Dispose();

                this.bNamePlate = new Bitmap( 0x170, 0x103 );

                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.DrawImage(this.iNamePlate, 0, 0, 0x170, 0x103);

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    Rectangle Rect1 = new Rectangle(7, 167, 150, 38);
                    Rectangle Rect2 = new Rectangle(0, 0 + (this.nDifficulty * 38), 150, 38);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    Rectangle Rect1 = new Rectangle(7, 138, 194, 60);
                    Rectangle Rect2 = new Rectangle(0, 0 + (this.nDifficulty * 60), 194, 60);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                    if (this.iPart != null)
                    {
                        Rectangle RectP = new Rectangle(0, 0, 194, 60);
                        gNamePlate.DrawImage(this.iPart, 7, 138, RectP, GraphicsUnit.Pixel);
                    }
                }

                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;

                #region[ ネームカラー ]
                //--------------------
                Color clNameColor = Color.White;
                Color clNameColorLower = Color.White;
                switch( CDTXMania.ConfigIni.nNameColor[ 0 ] )
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
                        clNameColor = Color.FromArgb( 0, 255, 33 );
                        clNameColorLower = Color.White;
                        break;
                    case 14:
                        clNameColor = Color.FromArgb( 0, 38, 255 );
                        clNameColorLower = Color.White;
                        break;
                    case 15:
                        clNameColor = Color.FromArgb( 72, 0, 255 );
                        clNameColorLower = Color.White;
                        break;
                    case 16:
                        clNameColor = Color.FromArgb( 255, 255, 0, 0 );
                        clNameColorLower = Color.White;
                        break;
                    case 17:
                        clNameColor = Color.FromArgb( 255, 232, 182, 149 );
                        clNameColorLower = Color.FromArgb( 255, 122, 69, 26 );
                        break;
                    case 18:
                        clNameColor = Color.FromArgb( 246, 245, 255 );
                        clNameColorLower = Color.FromArgb( 125, 128, 137 );
                        break;
                    case 19:
                        clNameColor = Color.FromArgb( 255, 238, 196, 85 );
                        clNameColorLower = Color.FromArgb(255, 255, 241, 200 );
                        break;
                }

                Bitmap bmpCardName = new Bitmap( 1, 1 );

                if (CDTXMania.ConfigIni.nNameColor[ 0 ] >= 11)
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent, clNameColor, clNameColorLower);
                }
                else
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent);
                }
                //--------------------
                #endregion

                #region[ 名前、グループ名 ]
                //2013.09.07.kairera0467 できればこの辺のメンテナンスが楽にできるよう、コードを簡略にしたいが・・・・
                Bitmap bmpSongTitle = new Bitmap( 1, 1 );
                #region[ 曲名 ]
                if( File.Exists( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" ) )
                {
                    Image imgCustomSongNameTexture;
                    imgCustomSongNameTexture = Image.FromFile( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" );
                    //2014.08.11 kairera0467 XG1とXG2では座標が異なるため、変数を使って対処する。
                    int x = CDTXMania.ConfigIni.eNamePlate == Eタイプ.A ? 78 : 80;
                    int y = CDTXMania.ConfigIni.eNamePlate == Eタイプ.A ? 59 : 50;
                    gNamePlate.DrawImage( imgCustomSongNameTexture, x, y, 238, 30 );
                }
                else
                {
                    if (this.nStrlengthbydot > 240)
                    {
                        gNamePlate.ScaleTransform(240.0f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append);
                        if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f / (240f / (float)this.nStrlengthbydot), 62f);
                            gNamePlate.ResetTransform();
                        }
                        else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f / (240f / (float)this.nStrlengthbydot), 54f);
                            gNamePlate.ResetTransform();
                        }
                    }
                    else
                    {
                        if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 65f);

                        }
                        else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 54f);
                        }
                    }
                }
                #endregion

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    gNamePlate.DrawImage(bmpCardName, 42f, 126f);
                    gNamePlate.DrawString(this.strGroupName, this.ftGroupFont, Brushes.White, 54f, 105f);
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    gNamePlate.DrawImage(bmpCardName, 46f, 92f);
                }
                #endregion

                string str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums) / 10f );
                if( CDTXMania.DTX.LEVEL.Drums > 100 )
                {
                    str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums) / 100f );
                }
                else
                {
                    str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums ) / 10.0f + ( CDTXMania.DTX.LEVELDEC.Drums != 0 ? CDTXMania.DTX.LEVELDEC.Drums / 100.0f : 0 ) );
                }
                
                if ( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                    ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) && 
                    ( CDTXMania.DTX.bチップがある.LP == false ) && 
                    ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                    ( CDTXMania.DTX.bチップがある.FT == false ) && 
                    ( CDTXMania.DTX.bチップがある.Ride == false ) &&
                    ( CDTXMania.DTX.b強制的にXG譜面にする == false) )
                {
                    str = string.Format( "{0:00}", CDTXMania.DTX.LEVEL.Drums );
                }


                int width = (int)gNamePlate.MeasureString(this.stパネルマップ[this.nIndex].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                //数字の描画部分。その左側。
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                    ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) && 
                    ( CDTXMania.DTX.bチップがある.LP == false ) && 
                    ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                    ( CDTXMania.DTX.bチップがある.FT == false ) && 
                    ( CDTXMania.DTX.bチップがある.Ride == false ) &&
                    ( CDTXMania.DTX.b強制的にXG譜面にする == false) )
                {
                    if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 18f + width, 164f);
                    else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 24f + width, 154f);
                }
                else
                {
                    if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 12.0f + width, 164f);

                    else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 14.0f + width, 130f);
                }
                width += ( int )gNamePlate.MeasureString( str.Substring( 0, 1 ), this.ftDifficultyL ).Width;

                //数字の右。
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                    ( CDTXMania.DTX.bチップがある.LeftCymbal == false ) && 
                    ( CDTXMania.DTX.bチップがある.LP == false ) && 
                    ( CDTXMania.DTX.bチップがある.LBD == false ) && 
                    ( CDTXMania.DTX.bチップがある.FT == false ) && 
                    ( CDTXMania.DTX.bチップがある.Ride == false ) &&
                    ( CDTXMania.DTX.b強制的にXG譜面にする == false) )
                {
                }
                else
                {
                    if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, width, 176f);
                    else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 2f + width, 166f);
                }

                //ジャケット画像描画部
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                {
                    this.iAlbum = Image.FromFile( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                }
                else
                {
                    this.iAlbum = Image.FromFile( path );
                }

                if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 0x11, 0x45, 0x4b ),  new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 209, 156, 42, 48 ),     new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 156, 42, 48 ),     new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
                else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 9, 0x45, 0x4b ), new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 141, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
                gNamePlate.Dispose();
                bmpCardName.Dispose();
                bmpSongTitle.Dispose();
                b4font.Dispose();
                //テクスチャ変換
                this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );

                //
                this.iAlbum.Dispose();
                this.iNamePlate.Dispose();
                this.iDifficulty.Dispose();
                this.iPart.Dispose();

                //ここで使用したフォント3つはここで開放。
                this.ftLevelFont.Dispose();
                this.ftDisplayFont.Dispose();
                this.pfNameFont.Dispose();
                this.pfSongTitleFont.Dispose();
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if( !base.b活性化してない )
            {
                //テクスチャ5枚
                //イメージ 6枚(ジャケット画像はここで解放しない)
                //フォント 5個
                CDTXMania.tテクスチャの解放( ref this.txNamePlate );
                CDTXMania.tテクスチャの解放( ref this.txScore );
                this.iNamePlate.Dispose();
                this.iDrumspeed.Dispose();
                this.iRisky.Dispose();
                this.iDifficulty.Dispose();
                this.iPart.Dispose();

                this.ftDifficultyS.Dispose();
                this.ftDifficultyL.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftLevelFont.Dispose();
                this.pfNameFont.Dispose();
                pfSongTitleFont.Dispose();
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( !base.b活性化してない )
            {
                SlimDX.Matrix identity = SlimDX.Matrix.Identity;
                if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                {
                    identity *= SlimDX.Matrix.Translation( -1135, 150, 0 );
                    identity *= SlimDX.Matrix.Scaling( 0.338f, 0.62f, 1f );
                    identity *= SlimDX.Matrix.RotationY( -0.8f );
                }
                else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                {
                    identity *= SlimDX.Matrix.Translation( -991, 225, 0 );
                    identity *= SlimDX.Matrix.Scaling( 0.385f, 0.61f, 1.0f );
                    identity *= SlimDX.Matrix.RotationY( -0.60f );
                }

                if ( CDTXMania.ConfigIni.bShowMusicInfo )
                    this.txNamePlate.t3D描画( CDTXMania.app.Device, identity );

                if ( this.nCurrentDrumspeed != CDTXMania.ConfigIni.n譜面スクロール速度.Drums )
                {
                    Graphics gNamePlate = Graphics.FromImage( this.bNamePlate );
                    this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                    if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                    {
                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 209, 156, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 156, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                    }
                    else if(CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                    {
                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 141, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                    }
                    gNamePlate.Dispose();
                    this.txNamePlate.Dispose();
                    this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );
                }

                #region[ スコア表示 ]
                this.nCurrentScore = (long)CDTXMania.stage演奏ドラム画面.actScore.n現在表示中のスコア.Drums;
                if ( CDTXMania.ConfigIni.nSkillMode == 0 && CDTXMania.ConfigIni.bShowScore )
                {
                    string str = this.nCurrentScore.ToString("0000000000");
                    for (int i = 0; i < 10; i++)
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if (this.txScore != null)
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation((-615f + (i * 21f)) / 0.7f, 280, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                            }
                            else if ( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1220 + (i * 30), 120 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.34f, 0.62f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                            }
                            else if ( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.3f, 0.62f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                        }
                    }
                }
                else if ( CDTXMania.ConfigIni.nSkillMode == 1 && CDTXMania.ConfigIni.bShowScore )
                {
                    string str = this.nCurrentScore.ToString("0000000");
                    for( int i = 0; i < 7; i++ )
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if( this.txScore != null )
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-610 + (i * 30), 280, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(1f, 1f, 1f);
                            }
                            else if ( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-870 + (i * 30), 114 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.47f, 0.65f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                            }
                            else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-974 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.42f, 0.62f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                        }
                    }
                }
                #endregion
            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iAlbum;
        private Image iDrumspeed;
        private Image iRisky;
        private Image iNamePlate;
        private Image iDifficulty;
        private Image iPart;
        private int nDifficulty;
        private int nCurrentDrumspeed;
        private int nStrlengthbydot;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private CTexture txNamePlate;
        private CTexture txScore;
        private CPrivateFastFont pfNameFont;
        private CPrivateFastFont pfSongTitleFont;
        //-----------------
        #endregion
    }
}
