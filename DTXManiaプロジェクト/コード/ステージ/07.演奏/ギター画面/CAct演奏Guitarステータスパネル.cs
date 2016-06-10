﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
    {

        public CAct演奏Guitarステータスパネル()
        {
            this.txパネル文字 = new CTexture[2];
            ST文字位置[] st文字位置Array = new ST文字位置[11];
            ST文字位置 st文字位置 = new ST文字位置();
            st文字位置.ch = '0';
            st文字位置.pt = new Point(0, 0);
            st文字位置Array[0] = st文字位置;
            ST文字位置 st文字位置2 = new ST文字位置();
            st文字位置2.ch = '1';
            st文字位置2.pt = new Point(28, 0);
            st文字位置Array[1] = st文字位置2;
            ST文字位置 st文字位置3 = new ST文字位置();
            st文字位置3.ch = '2';
            st文字位置3.pt = new Point(56, 0);
            st文字位置Array[2] = st文字位置3;
            ST文字位置 st文字位置4 = new ST文字位置();
            st文字位置4.ch = '3';
            st文字位置4.pt = new Point(84, 0);
            st文字位置Array[3] = st文字位置4;
            ST文字位置 st文字位置5 = new ST文字位置();
            st文字位置5.ch = '4';
            st文字位置5.pt = new Point(112, 0);
            st文字位置Array[4] = st文字位置5;
            ST文字位置 st文字位置6 = new ST文字位置();
            st文字位置6.ch = '5';
            st文字位置6.pt = new Point(140, 0);
            st文字位置Array[5] = st文字位置6;
            ST文字位置 st文字位置7 = new ST文字位置();
            st文字位置7.ch = '6';
            st文字位置7.pt = new Point(168, 0);
            st文字位置Array[6] = st文字位置7;
            ST文字位置 st文字位置8 = new ST文字位置();
            st文字位置8.ch = '7';
            st文字位置8.pt = new Point(196, 0);
            st文字位置Array[7] = st文字位置8;
            ST文字位置 st文字位置9 = new ST文字位置();
            st文字位置9.ch = '8';
            st文字位置9.pt = new Point(224, 0);
            st文字位置Array[8] = st文字位置9;
            ST文字位置 st文字位置10 = new ST文字位置();
            st文字位置10.ch = '9';
            st文字位置10.pt = new Point(252, 0);
            st文字位置Array[9] = st文字位置10;
            ST文字位置 st文字位置11 = new ST文字位置();
            st文字位置11.ch = '.';
            st文字位置11.pt = new Point(280, 0);
            st文字位置Array[10] = st文字位置11;
            this.st大文字位置 = st文字位置Array;

            ST文字位置[] st文字位置Array2 = new ST文字位置[12];
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '0';
            st文字位置13.pt = new Point(0, 0);
            st文字位置Array2[0] = st文字位置13;
            ST文字位置 st文字位置14 = new ST文字位置();
            st文字位置14.ch = '1';
            st文字位置14.pt = new Point(20, 0);
            st文字位置Array2[1] = st文字位置14;
            ST文字位置 st文字位置15 = new ST文字位置();
            st文字位置15.ch = '2';
            st文字位置15.pt = new Point(40, 0);
            st文字位置Array2[2] = st文字位置15;
            ST文字位置 st文字位置16 = new ST文字位置();
            st文字位置16.ch = '3';
            st文字位置16.pt = new Point(60, 0);
            st文字位置Array2[3] = st文字位置16;
            ST文字位置 st文字位置17 = new ST文字位置();
            st文字位置17.ch = '4';
            st文字位置17.pt = new Point(80, 0);
            st文字位置Array2[4] = st文字位置17;
            ST文字位置 st文字位置18 = new ST文字位置();
            st文字位置18.ch = '5';
            st文字位置18.pt = new Point(100, 0);
            st文字位置Array2[5] = st文字位置18;
            ST文字位置 st文字位置19 = new ST文字位置();
            st文字位置19.ch = '6';
            st文字位置19.pt = new Point(120, 0);
            st文字位置Array2[6] = st文字位置19;
            ST文字位置 st文字位置20 = new ST文字位置();
            st文字位置20.ch = '7';
            st文字位置20.pt = new Point(140, 0);
            st文字位置Array2[7] = st文字位置20;
            ST文字位置 st文字位置21 = new ST文字位置();
            st文字位置21.ch = '8';
            st文字位置21.pt = new Point(160, 0);
            st文字位置Array2[8] = st文字位置21;
            ST文字位置 st文字位置22 = new ST文字位置();
            st文字位置22.ch = '9';
            st文字位置22.pt = new Point(180, 0);
            st文字位置Array2[9] = st文字位置22;
            ST文字位置 st文字位置23 = new ST文字位置();
            st文字位置23.ch = '%';
            st文字位置23.pt = new Point(200, 0);
            st文字位置Array2[10] = st文字位置23;
            ST文字位置 st文字位置24 = new ST文字位置();
            st文字位置24.ch = '.';
            st文字位置24.pt = new Point(210, 0);
            st文字位置Array2[11] = st文字位置24;
            this.st小文字位置 = st文字位置Array2;

            ST文字位置[] st難易度文字位置Ar = new ST文字位置[11];
            ST文字位置 st難易度文字位置 = new ST文字位置();
            st難易度文字位置.ch = '0';
            st難易度文字位置.pt = new Point(0, 0);
            st難易度文字位置Ar[0] = st難易度文字位置;
            ST文字位置 st難易度文字位置2 = new ST文字位置();
            st難易度文字位置2.ch = '1';
            st難易度文字位置2.pt = new Point(16, 0);
            st難易度文字位置Ar[1] = st難易度文字位置2;
            ST文字位置 st難易度文字位置3 = new ST文字位置();
            st難易度文字位置3.ch = '2';
            st難易度文字位置3.pt = new Point(32, 0);
            st難易度文字位置Ar[2] = st難易度文字位置3;
            ST文字位置 st難易度文字位置4 = new ST文字位置();
            st難易度文字位置4.ch = '3';
            st難易度文字位置4.pt = new Point(48, 0);
            st難易度文字位置Ar[3] = st難易度文字位置4;
            ST文字位置 st難易度文字位置5 = new ST文字位置();
            st難易度文字位置5.ch = '4';
            st難易度文字位置5.pt = new Point(64, 0);
            st難易度文字位置Ar[4] = st難易度文字位置5;
            ST文字位置 st難易度文字位置6 = new ST文字位置();
            st難易度文字位置6.ch = '5';
            st難易度文字位置6.pt = new Point(80, 0);
            st難易度文字位置Ar[5] = st難易度文字位置6;
            ST文字位置 st難易度文字位置7 = new ST文字位置();
            st難易度文字位置7.ch = '6';
            st難易度文字位置7.pt = new Point(96, 0);
            st難易度文字位置Ar[6] = st難易度文字位置7;
            ST文字位置 st難易度文字位置8 = new ST文字位置();
            st難易度文字位置8.ch = '7';
            st難易度文字位置8.pt = new Point(112, 0);
            st難易度文字位置Ar[7] = st難易度文字位置8;
            ST文字位置 st難易度文字位置9 = new ST文字位置();
            st難易度文字位置9.ch = '8';
            st難易度文字位置9.pt = new Point(128, 0);
            st難易度文字位置Ar[8] = st難易度文字位置9;
            ST文字位置 st難易度文字位置10 = new ST文字位置();
            st難易度文字位置10.ch = '9';
            st難易度文字位置10.pt = new Point(144, 0);
            st難易度文字位置Ar[9] = st難易度文字位置10;
            ST文字位置 st難易度文字位置11 = new ST文字位置();
            st難易度文字位置11.ch = '.';
            st難易度文字位置11.pt = new Point(160, 0);
            st難易度文字位置Ar[10] = st難易度文字位置11;
            this.st難易度数字位置 = st難易度文字位置Ar;

            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            #region [ 本体位置 ]
            this.n本体X[0] = 0;
            this.n本体X[1] = 373;
            this.n本体X[2] = 665;
            this.n本体Y = 270;

            if (!CDTXMania.DTX.bチップがある.Bass)
            {
                if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                {
                    this.n本体X[1] = 0;
                }
                else
                {
                    this.n本体X[2] = 0;
                }
            }
            else if (!CDTXMania.DTX.bチップがある.Guitar)
            {
                if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                {
                    this.n本体X[2] = 0;
                }
                else
                {
                    this.n本体X[1] = 0;
                }
            }
            else if (CDTXMania.ConfigIni.bGraph有効.Guitar || CDTXMania.ConfigIni.bGraph有効.Bass )
            {
                if (!CDTXMania.ConfigIni.bギターが全部オートプレイである && CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {
                    this.n本体X[2] = 0;
                }
                else if (CDTXMania.ConfigIni.bギターが全部オートプレイである && !CDTXMania.ConfigIni.bベースが全部オートプレイである)
                {
                    this.n本体X[1] = 0;
                }
            }
            #endregion
            this.strPlayerName = new string[ 2 ];
            this.strTitleName = new string[ 2 ];
            
            this.prv表示用フォント = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str曲名表示フォント ), 20, FontStyle.Regular );
            this.prv称号フォント = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str曲名表示フォント ), 12, FontStyle.Regular );
            this.txスキルパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_SkillPanel.png"));
            this.txパネル文字[0] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Ratenumber_s.png"));
            this.txパネル文字[1] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Ratenumber_l.png"));
            this.tx難易度パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Difficulty.png"));
            this.tx難易度用数字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_LevelNumber.png"));

            base.On活性化();
        }
        public override void On非活性化()
        {
            if (this.ft称号フォント != null)
            {
                this.ft称号フォント.Dispose();
                this.ft称号フォント = null;
            }
            CDTXMania.tテクスチャの解放(ref this.txスキルパネル);
            CDTXMania.tテクスチャの解放(ref this.txパネル文字[0]);
            CDTXMania.tテクスチャの解放(ref this.txパネル文字[1]);
            CDTXMania.tテクスチャの解放(ref this.tx難易度パネル);
            CDTXMania.tテクスチャの解放(ref this.tx難易度用数字);
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txネームプレート用文字 = new CTexture[ 2 ];
                this.strPlayerName[ 0 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[ 1 ] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[ 1 ];
                this.strPlayerName[ 1 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[ 2 ] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[ 2 ];
                this.strTitleName[ 0 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[ 1 ] ) ? "" : CDTXMania.ConfigIni.strGroupName[ 1 ];
                this.strTitleName[ 1 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[ 2 ] ) ? "" : CDTXMania.ConfigIni.strGroupName[ 2 ];

                for( int i = 0; i < 2; i++ )
                {
                    Bitmap image2 = new Bitmap( 200, 100 );
                    Graphics graネームプレート用 = Graphics.FromImage(image2);

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
                    //--------------------
                    #endregion
                    #region[ 名前とか ]
                    Bitmap bmpCardName = new Bitmap(1, 1);
                    bmpCardName = this.prv表示用フォント.DrawPrivateFont( this.strPlayerName[ i ], Color.White, Color.Transparent, clNameColor, ( CDTXMania.ConfigIni.nNameColor[ 0 ] > 11 ? clNameColorLower : clNameColor ) );
                    Bitmap bmpTitleName = new Bitmap(1, 1);
                    bmpTitleName = this.prv称号フォント.DrawPrivateFont( this.strTitleName[ i ], Color.White, Color.Transparent );

                    graネームプレート用.DrawImage( bmpCardName, -2f, 26f );
                    graネームプレート用.DrawImage( bmpTitleName, 6f, 8f );
                    #endregion

                    this.prv表示用フォント.Dispose();
                    this.prv称号フォント.Dispose();
                    bmpCardName.Dispose();
                    bmpTitleName.Dispose();

                    this.txネームプレート用文字[ i ] = new CTexture( CDTXMania.app.Device, image2, CDTXMania.TextureFormat, false );
                    image2.Dispose();
                }
                this.prv表示用フォント.Dispose();

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放( ref this.txネームプレート用文字[ 0 ] );
                CDTXMania.tテクスチャの解放( ref this.txネームプレート用文字[ 1 ] );
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                double dbPERFECT率 = 0;
                double dbGREAT率 = 0;
                double dbGOOD率 = 0;
                double dbPOOR率 = 0;
                double dbMISS率 = 0;
                double dbMAXCOMBO率 = 0;

                for( int i = 1; i < 3; i++ )
                {
                    if (this.n本体X[ i ] != 0)
                    {
                        string str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL[ i ] ) / 10f );
                        bool bCLASSIC = false;
                        if( CDTXMania.DTX.LEVEL[ i ] > 100 )
                        {
                            str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL[ i ] ) / 100f );
                        }
                        else
                        {
                            str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL[ i ] ) / 10.0f + ( CDTXMania.DTX.LEVELDEC[ i ] != 0 ? CDTXMania.DTX.LEVELDEC[ i ] / 100.0f : 0 ) );
                        }

                        if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                            ( i == 1 ? !CDTXMania.DTX.bチップがある.YPGuitar : !CDTXMania.DTX.bチップがある.YPBass ) &&
                            ( CDTXMania.DTX.b強制的にXG譜面にする == false ) )
                        {
                            str = string.Format( "{0:00}", CDTXMania.DTX.LEVEL[ i ] );
                            bCLASSIC = true;
                        }

                        this.txスキルパネル.t2D描画( CDTXMania.app.Device, this.n本体X[ i ], this.n本体Y );
                        this.txネームプレート用文字[ i - 1 ].t2D描画( CDTXMania.app.Device, this.n本体X[ i ], this.n本体Y );

                        this.t小文字表示( 80 + this.n本体X[ i ], 72 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Perfect ) );
                        this.t小文字表示( 80 + this.n本体X[ i ], 102 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Great ) );
                        this.t小文字表示( 80 + this.n本体X[ i ], 132 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Good ) );
                        this.t小文字表示( 80 + this.n本体X[ i ], 162 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Poor ) );
                        this.t小文字表示( 80 + this.n本体X[ i ], 192 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Miss ) );
                        this.t小文字表示( 80 + this.n本体X[ i ], 222 + this.n本体Y, string.Format( "{0,4:###0}", CDTXMania.stage演奏ギター画面.actCombo.n現在のコンボ数.最高値[ i ] ) );

                        int n現在のノーツ数 =
                            CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[ i ].Perfect +
                            CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[ i ].Great +
                            CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[ i ].Good +
                            CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[ i ].Poor +
                            CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[ i ].Miss;

                        dbPERFECT率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Perfect) / n現在のノーツ数 );
                        dbGREAT率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Great / n現在のノーツ数 ) );
                        dbGOOD率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Good / n現在のノーツ数 ) );
                        dbPOOR率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Poor / n現在のノーツ数 ) );
                        dbMISS率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[ i ].Miss / n現在のノーツ数 ) );
                        dbMAXCOMBO率 = Math.Round( ( 100.0 * CDTXMania.stage演奏ギター画面.actCombo.n現在のコンボ数.最高値[ i ] / n現在のノーツ数 ) );

                        if( double.IsNaN( dbPERFECT率 ) )
                            dbPERFECT率 = 0;
                        if( double.IsNaN( dbGREAT率 ) )
                            dbGREAT率 = 0;
                        if( double.IsNaN( dbGOOD率 ) )
                            dbGOOD率 = 0;
                        if( double.IsNaN( dbPOOR率 ) )
                            dbPOOR率 = 0;
                        if( double.IsNaN( dbMISS率 ) )
                            dbMISS率 = 0;
                        if( double.IsNaN( dbMAXCOMBO率 ) )
                            dbMAXCOMBO率 = 0;

                        this.t小文字表示( 167 + this.n本体X[ i ], 72 + this.n本体Y, string.Format( "{0,3:##0}%", dbPERFECT率 ) );
                        this.t小文字表示( 167 + this.n本体X[ i ], 102 + this.n本体Y, string.Format( "{0,3:##0}%", dbGREAT率 ) );
                        this.t小文字表示( 167 + this.n本体X[ i ], 132 + this.n本体Y, string.Format( "{0,3:##0}%", dbGOOD率 ) );
                        this.t小文字表示( 167 + this.n本体X[ i ], 162 + this.n本体Y, string.Format( "{0,3:##0}%", dbPOOR率 ) );
                        this.t小文字表示( 167 + this.n本体X[ i ], 192 + this.n本体Y, string.Format( "{0,3:##0}%", dbMISS率 ) );
                        this.t小文字表示( 167 + this.n本体X[ i ], 222 + this.n本体Y, string.Format( "{0,3:##0}%", dbMAXCOMBO率 ) );

                this.t大文字表示(58 + this.n本体X[ i ], 277 + this.n本体Y, string.Format( "{0,6:##0.00}", CDTXMania.stage演奏ギター画面.actStatusPanels.db現在の達成率.Guitar ) );
                this.t大文字表示(88 + this.n本体X[ i ], 363 + this.n本体Y, string.Format( "{0,6:##0.00}", CDTXMania.stage演奏ギター画面.actStatusPanels.db現在の達成率.Guitar * ( CDTXMania.DTX.LEVEL[ i ] / 10.0 ) * 0.2 ) );

                if( this.tx難易度パネル != null )
                    this.tx難易度パネル.t2D描画( CDTXMania.app.Device, 14 + this.n本体X[ i ], 266 + this.n本体Y, new Rectangle( base.rectDiffPanelPoint.X, base.rectDiffPanelPoint.Y, 60, 60 ) );
                this.tレベル数字描画( ( bCLASSIC == true ? 26 : 18 ) + this.n本体X[ i ], 290 + this.n本体Y, str );
                    }
                }
            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
        }
        private STDGBVALUE<int> n本体X;
        private int n本体Y;
        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;
        private readonly ST文字位置[] st難易度数字位置;
        private CTexture txスキルパネル;
        private CTexture[] txパネル文字;
        private CPrivateFastFont prv表示用フォント;
        private CPrivateFastFont prv称号フォント;
        private Font ft称号フォント;
        private string[] strPlayerName;
        private string[] strTitleName;
        private CTexture[] txネームプレート用文字;
        private CTexture tx難易度パネル;
        private CTexture tx難易度用数字;

        private void t小文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st小文字位置.Length; i++)
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 20, 26);
                        if (this.txパネル文字[0] != null)
                        {
                            this.txパネル文字[0].t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 20;
            }
        }
        private void t大文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st大文字位置.Length; i++)
                {
                    if (this.st大文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st大文字位置[i].pt.X, this.st大文字位置[i].pt.Y, 28, 42);
                        if (ch == '.')
                        {
                            rectangle.Width -= 18;
                        }
                        if (this.txパネル文字[1] != null)
                        {
                            this.txパネル文字[1].t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += (ch == '.' ? 12 : 29);
            }
        }
        private void tレベル数字描画(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st難易度数字位置.Length; i++)
                {
                    if (this.st難易度数字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st難易度数字位置[i].pt.X, this.st難易度数字位置[i].pt.Y, 16, 32);
                        if (ch == '.')
                        {
                            rectangle.Width -= 11;
                        }
                        if (this.tx難易度用数字 != null)
                        {
                            this.tx難易度用数字.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += (ch == '.' ? 5 : 16);
            }
        }

        //-----------------
        #endregion
    }
}
