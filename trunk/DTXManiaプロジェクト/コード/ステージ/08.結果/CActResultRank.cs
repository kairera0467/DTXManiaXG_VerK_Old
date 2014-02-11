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

            this.n本体X[0] = 0;
            this.n本体Y[0] = 0;

            this.n本体X[1] = 0;
            this.n本体Y[1] = 0;

            this.n本体X[2] = 0;
            this.n本体Y[2] = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = n上X;
                this.n本体Y[0] = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = n下X;
                        this.n本体Y[1] = n下Y;
                    }
                    else
                    {
                        this.n本体X[1] = n上X;
                        this.n本体Y[1] = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = n上X;
                        this.n本体Y[2] = n上Y;
                    }
                    else
                    {
                        this.n本体X[2] = n下X;
                        this.n本体Y[2] = n下Y;
                    }
                }

            }
            #endregion

            this.b全オート.Drums = CDTXMania.ConfigIni.bドラムが全部オートプレイである;
            this.b全オート.Guitar = CDTXMania.ConfigIni.bギターが全部オートプレイである;
            this.b全オート.Bass = CDTXMania.ConfigIni.bベースが全部オートプレイである;

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

                for (int j = 0; j < 3; j++)
                {
                    switch (CDTXMania.stage結果.nランク値[j])
                    {
                        case 0:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            if (this.b全オート[j])
                                this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        default:
                            this.txランク文字[j] = null;
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
                CDTXMania.tテクスチャの解放( ref this.txFullCombo );
                CDTXMania.tテクスチャの解放( ref this.txExcellent );
                CDTXMania.t安全にDisposeする( ref this.txランク文字 );
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
                for (int j = 0; j < 3; j++)
                {
                    if (this.txランク文字[j] != null && this.n本体X[j] != 0)
                    {
                        this.txランク文字[j].t2D描画(CDTXMania.app.Device, this.n本体X[j], this.n本体Y[j], new Rectangle(0, 0, (int)((double)txランク文字[j].sz画像サイズ.Width * num2), this.txランク文字[j].sz画像サイズ.Height));
                    }
                }
			}

            #region [ フルコンボ ]
            for (int j = 0; j < 3; j++)
            {   
                
                if (this.ct表示用.n現在の値 >= 900)
                {
                    int num14 = 82 + this.n本体X[j];
                    int num15 = 152 + this.n本体Y[j];

                    if (this.n本体X[j] != 0)
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
                                    num14 = this.n本体X[j] + ((int)((this.txFullCombo.sz画像サイズ.Width * (1f - num13)) / 2f));
                                    num15 = this.n本体Y[j] + ((int)((this.txFullCombo.sz画像サイズ.Height * (1f - num13)) / 2f));

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
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private STDGBVALUE<bool> b全オート;
        private CSound sdDTXで指定されたフルコンボ音;
        private STDGBVALUE<CTexture> txランク文字;
        private CTexture txExcellent;
        private CTexture txFullCombo;
        //-----------------
		#endregion

	}
}
