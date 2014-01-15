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
            this.n本体1X = 0;
            this.n本体1Y = 0;

            this.n本体2X = 0;
            this.n本体2Y = 0;

            this.n本体3X = 0;
            this.n本体3Y = 0;


            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体1X = 138;
                this.n本体1Y = 8;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    this.n本体1X = 138;
                    this.n本体1Y = 8;
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    this.n本体2X = 850;
                    this.n本体2Y = 420;
                }

                if (CDTXMania.ConfigIni.bIsSwappedGuitarBass && CDTXMania.DTX.bチップがある.Guitar && CDTXMania.DTX.bチップがある.Bass)
                {
                    this.n本体3X = this.n本体1X;
                    this.n本体3Y = this.n本体1Y;
                    this.n本体1X = this.n本体2X;
                    this.n本体1Y = this.n本体2Y;
                    this.n本体2X = this.n本体3X;
                    this.n本体2Y = this.n本体3Y;
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
                    switch (CDTXMania.stage結果.n総合ランク値)
                    {
                        case 0:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            break;

                        default:
                            this.txランク文字 = null;
                            break;
                    }
                }
                else if( CDTXMania.ConfigIni.bGuitar有効 )
                {
                    switch( CDTXMania.stage結果.nランク値.Guitar )
                    {
                        case 0:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankE.png" ) );
                            if( CDTXMania.ConfigIni.bギターが全部オートプレイである )
                                this.txランク文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_rankSS.png" ) );
                            break;

                        default:
                            this.txランク文字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
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
				CDTXMania.tテクスチャの解放( ref this.txランク文字 );
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
				base.b初めての進行描画 = false;
			}
			this.ctランク表示.t進行();
			if( this.ctランク表示.n現在の値 >= 500 )
			{
				double num2 = ( (double) ( this.ctランク表示.n現在の値 - 500 ) ) / 500.0;
                if ( this.txランク文字 != null && this.n本体1Y != 0 )
                {
                    this.txランク文字.t2D描画(CDTXMania.app.Device, this.n本体1X, this.n本体1Y, new Rectangle(0, 0, (int)((double)txランク文字.sz画像サイズ.Width * num2), this.txランク文字.sz画像サイズ.Height));
                }
                if ( this.txランク文字2P != null && this.n本体2Y != 0 )
                {
                    this.txランク文字2P.t2D描画(CDTXMania.app.Device, this.n本体2X, this.n本体2Y, new Rectangle(0, 0, (int)((double)txランク文字.sz画像サイズ.Width * num2), this.txランク文字.sz画像サイズ.Height));
                }
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
		private int n本体1X;
		private int n本体1Y;
        private int n本体2X;
        private int n本体2Y;
        private int n本体3X;
        private int n本体3Y;
        private CTexture txランク文字;
        private CTexture txランク文字2P;
		//-----------------
		#endregion
	}
}
