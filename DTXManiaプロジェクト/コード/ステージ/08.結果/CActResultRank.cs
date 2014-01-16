using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultRank : CActivity
	{

		// コンストラクタ

		public CActResultRank()
		{
            base.b活性化してない = true;
		}

		// メソッド

		public void tアニメを完了させる()
		{
			this.ctランク表示.n現在の値 = this.ctランク表示.n終了値;
            this.ct表示用.n現在の値 = this.ct表示用.n終了値;
        }

		// CActivity 実装

		public override void On活性化()
		{

            #region [ 本体位置 ]

            int n上X = 138;
            int n上Y = 8;

            int n下X = 850;
            int n下Y = 420;

            this.n本体0X = 0;
            this.n本体0Y = 0;

            this.n本体1X = 0;
            this.n本体1Y = 0;

            this.n本体2X = 0;
            this.n本体2Y = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体0X = n上X;
                this.n本体0Y = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体1X = n下X;
                        this.n本体1Y = n下Y;
                    }
                    else
                    {
                        this.n本体1X = n上X;
                        this.n本体1Y = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体2X = n上X;
                        this.n本体2Y = n上Y;
                    }
                    else
                    {
                        this.n本体2X = n下X;
                        this.n本体2Y = n下Y;
                    }
                }

            }
            #endregion

            this.sdDTXで指定されたフルコンボ音 = null;
            this.bフルコンボ音再生済み = false;
            this.bエクセレント音再生済み = false;
            base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ctランク表示 != null )
			{
				this.ctランク表示 = null;
			}
            if (this.ct表示用 != null)
            {
                this.ct表示用 = null;
            }
            if (this.sdDTXで指定されたフルコンボ音 != null)
            {
                CDTXMania.Sound管理.tサウンドを破棄する(this.sdDTXで指定されたフルコンボ音);
                this.sdDTXで指定されたフルコンボ音 = null;
            }
            base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{

                this.txFullCombo = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenResult fullcombo.png"));
                this.txExcellent = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenResult Excellent.png"));

                if (CDTXMania.ConfigIni.bDrums有効)
                {
                    switch (CDTXMania.stage結果.nランク値.Drums)
                    {
                        case 0:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            if (CDTXMania.ConfigIni.bドラムが全部オートプレイである)
                                this.txランク文字0P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        default:
                            this.txランク文字0P = null;
                            break;
                    }
                }
                else if( CDTXMania.ConfigIni.bGuitar有効 )
                {
                    switch( CDTXMania.stage結果.nランク値.Guitar )
                    {
                        case 0:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字1P = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankE.png" ) );
                            if( CDTXMania.ConfigIni.bギターが全部オートプレイである )
                                this.txランク文字1P = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankSS.png" ) );
                            break;

                        default:
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            break;
                    }
                    switch (CDTXMania.stage結果.nランク値.Bass)
                    {
                        case 0:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            if (CDTXMania.ConfigIni.bベースが全部オートプレイである)
                                this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        default:
                            this.txランク文字2P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            break;
                    }
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放(ref this.txFullCombo);
                CDTXMania.tテクスチャの解放(ref this.txExcellent);
                CDTXMania.tテクスチャの解放(ref this.txランク文字0P);
                CDTXMania.tテクスチャの解放(ref this.txランク文字1P);
                CDTXMania.tテクスチャの解放( ref this.txランク文字2P );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
                this.ctランク表示 = new CCounter(0, 0x3e8, 2, CDTXMania.Timer);
                this.ct表示用 = new CCounter(0, 1000, 3, CDTXMania.Timer);
                base.b初めての進行描画 = false;
			}
			this.ctランク表示.t進行();
            this.ct表示用.t進行();
            if (this.ctランク表示.n現在の値 >= 500)
			{
				double num2 = ( (double) ( this.ctランク表示.n現在の値 - 500 ) ) / 500.0;
                if (this.txランク文字0P != null && this.n本体0Y != 0)
                {
                    this.txランク文字0P.t2D描画(CDTXMania.app.Device, this.n本体0X, this.n本体0Y, new Rectangle(0, 0, (int)((double)txランク文字0P.sz画像サイズ.Width * num2), this.txランク文字0P.sz画像サイズ.Height));
                }
                if (this.txランク文字1P != null && this.n本体1Y != 0)
                {
                    this.txランク文字1P.t2D描画(CDTXMania.app.Device, this.n本体1X, this.n本体1Y, new Rectangle(0, 0, (int)((double)txランク文字1P.sz画像サイズ.Width * num2), this.txランク文字1P.sz画像サイズ.Height));
                }
                if ( this.txランク文字2P != null && this.n本体2Y != 0 )
                {
                    this.txランク文字2P.t2D描画(CDTXMania.app.Device, this.n本体2X, this.n本体2Y, new Rectangle(0, 0, (int)((double)txランク文字2P.sz画像サイズ.Width * num2), this.txランク文字2P.sz画像サイズ.Height));
                }
			}

            int[] x = new int[3];
            int[] y = new int[3];

            x[0] = n本体0X;
            x[1] = n本体1X;
            x[2] = n本体2X;

            y[0] = n本体0Y;
            y[1] = n本体1Y;
            y[2] = n本体2Y;

            #region [ フルコンボ ]
            if (this.ct表示用.n現在の値 >= 900)
            {

                for (int j = 0; j < 3; j++)
                {

                    int num14 = 82 + x[j];
                    int num15 = 152 + y[j];

                    if ( x[j] != 0 )
                    {
                        if (CDTXMania.stage結果.st演奏記録[j].nPerfect数 == CDTXMania.stage結果.st演奏記録[j].n全チップ数)
                        {
                            if (this.ct表示用.b終了値に達した)
                            {
                                if (this.txExcellent != null)
                                {
                                    this.txExcellent.t2D描画(CDTXMania.app.Device, num14, num15);
                                }
                                if (!this.bエクセレント音再生済み)
                                {
                                    if (((CDTXMania.DTX.SOUND_FULLCOMBO != null) && (CDTXMania.DTX.SOUND_FULLCOMBO.Length > 0)) && File.Exists(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO))
                                    {
                                        try
                                        {
                                            if (this.sdDTXで指定されたフルコンボ音 != null)
                                            {
                                                CDTXMania.Sound管理.tサウンドを破棄する(this.sdDTXで指定されたフルコンボ音);
                                                this.sdDTXで指定されたフルコンボ音 = null;
                                            }
                                            this.sdDTXで指定されたフルコンボ音 = CDTXMania.Sound管理.tサウンドを生成する(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO);
                                            if (this.sdDTXで指定されたフルコンボ音 != null)
                                            {
                                                this.sdDTXで指定されたフルコンボ音.t再生を開始する();
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    else
                                    {
                                        CDTXMania.Skin.soundエクセレント音.t再生する();
                                    }
                                    this.bエクセレント音再生済み = true;
                                }
                            }
                            else
                            {
                                double num12 = ((double)(this.ct表示用.n現在の値 - 900)) / 100.0;
                                float num13 = (float)(1.1 - 0.1);
                                if (this.txExcellent != null)
                                {
                                    this.txExcellent.vc拡大縮小倍率 = new Vector3(num13, num13, 1f);
                                    this.txExcellent.n透明度 = (int)(255.0 * num12);
                                    this.txExcellent.t2D描画(CDTXMania.app.Device, num14, num15);
                                }
                            }
                        }
                        else if (CDTXMania.stage結果.st演奏記録[j].bフルコンボである && CDTXMania.stage結果.st演奏記録[j].nPerfect数 != CDTXMania.stage結果.st演奏記録[j].n全チップ数)
                        {
                            if (this.ct表示用.b終了値に達した)
                            {
                                if (this.txFullCombo != null)
                                {
                                    this.txFullCombo.t2D描画(CDTXMania.app.Device, num14, num15);
                                }
                                if (!this.bフルコンボ音再生済み)
                                {
                                    if (((CDTXMania.DTX.SOUND_FULLCOMBO != null) && (CDTXMania.DTX.SOUND_FULLCOMBO.Length > 0)) && File.Exists(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO))
                                    {
                                        try
                                        {
                                            if (this.sdDTXで指定されたフルコンボ音 != null)
                                            {
                                                CDTXMania.Sound管理.tサウンドを破棄する(this.sdDTXで指定されたフルコンボ音);
                                                this.sdDTXで指定されたフルコンボ音 = null;
                                            }
                                            this.sdDTXで指定されたフルコンボ音 = CDTXMania.Sound管理.tサウンドを生成する(CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.SOUND_FULLCOMBO);
                                            if (this.sdDTXで指定されたフルコンボ音 != null)
                                            {
                                                this.sdDTXで指定されたフルコンボ音.t再生を開始する();
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    else
                                    {
                                        CDTXMania.Skin.soundフルコンボ音.t再生する();
                                    }
                                    this.bフルコンボ音再生済み = true;
                                }
                            }
                            else
                            {
                                double num12 = ((double)(this.ct表示用.n現在の値 - 900)) / 100.0;
                                float num13 = (float)(1.1 - 0.1);
                                if (this.txFullCombo != null)
                                {
                                    this.txFullCombo.vc拡大縮小倍率 = new Vector3(num13, num13, 1f);
                                    this.txFullCombo.n透明度 = (int)(255.0 * num12);
                                    num14 = x[j] + ((int)((this.txFullCombo.sz画像サイズ.Width * (1f - num13)) / 2f));
                                    num15 = y[j] + ((int)((this.txFullCombo.sz画像サイズ.Height * (1f - num13)) / 2f));

                                    this.txFullCombo.t2D描画(CDTXMania.app.Device, num14, num15);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

			if( !this.ctランク表示.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
        private bool bフルコンボ音再生済み;
        private bool bエクセレント音再生済み;
        private CCounter ctランク表示;
        private CCounter ct表示用;
        private int n本体0X;
        private int n本体0Y;
        private int n本体1X;
		private int n本体1Y;
        private int n本体2X;
        private int n本体2Y;
        private CTexture txランク文字0P;
        private CTexture txランク文字1P;
        private CTexture txランク文字2P;
        private CSound sdDTXで指定されたフルコンボ音;
        private CTexture txExcellent;
        private CTexture txFullCombo;
        //-----------------
		#endregion

	}
}
