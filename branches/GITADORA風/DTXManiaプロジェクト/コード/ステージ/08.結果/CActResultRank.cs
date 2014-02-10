using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
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
		}


		// CActivity 実装

        public override void On活性化()
        {
            #region [ 本体位置 ]

            int n上X = 300;
            int n上Y = -10;

            int n下X = 950;
            int n下Y = -10;

            this.n本体0X = 0;
            this.n本体0Y = 0;

            this.n本体1X = 0;
            this.n本体1Y = 0;

            this.n本体2X = 0;
            this.n本体2Y = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体0X = 480;
                this.n本体0Y = 50;
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
            base.On活性化();
        }
		public override void On非活性化()
		{
			if( this.ctランク表示 != null )
			{
				this.ctランク表示 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
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
                else if (CDTXMania.ConfigIni.bGuitar有効)
                {
                    switch (CDTXMania.stage結果.nランク値.Guitar)
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
                            this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            if (CDTXMania.ConfigIni.bギターが全部オートプレイである)
                                this.txランク文字1P = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
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
                CDTXMania.tテクスチャの解放( ref this.txランク文字0P );
                CDTXMania.tテクスチャの解放( ref this.txランク文字1P );
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
                this.ctランク表示 = new CCounter(0, 127, 1, CDTXMania.Timer);
				base.b初めての進行描画 = false;
			}
			this.ctランク表示.t進行();
                if (this.txランク文字0P != null && this.n本体0X != 0)
                {
                    this.txランク文字0P.n透明度 = this.ctランク表示.n現在の値 * 2;
                    this.txランク文字0P.t2D描画(CDTXMania.app.Device, this.n本体0X, this.n本体0Y);
                }
                if (this.txランク文字1P != null && this.n本体1X != 0)
                {
                    this.txランク文字1P.n透明度 = this.ctランク表示.n現在の値 * 2;
                    this.txランク文字1P.t2D描画(CDTXMania.app.Device, this.n本体1X, this.n本体1Y);
                }
                if (this.txランク文字2P != null && this.n本体2X != 0)
                {
                    this.txランク文字2P.n透明度 = this.ctランク表示.n現在の値 * 2;
                    this.txランク文字2P.t2D描画(CDTXMania.app.Device, this.n本体2X, this.n本体2Y);
                }
			if( !this.ctランク表示.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
        private CCounter ctランク表示;
        private int n本体0X;
        private int n本体0Y;
        private int n本体1X;
        private int n本体1Y;
        private int n本体2X;
        private int n本体2Y;
        private CTexture txランク文字0P;
        private CTexture txランク文字1P;
        private CTexture txランク文字2P;
        //-----------------
		#endregion
	}
}
