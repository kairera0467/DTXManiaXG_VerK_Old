using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public STDGBVALUE<double> db現在のゲージ値;


		// コンストラクタ

		public CAct演奏Guitarゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct本体移動 = null;
			this.ct本体振動 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge_Guitar.png"));
                this.txgbarf = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));
                this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txgbar );
                CDTXMania.tテクスチャの解放( ref this.txgbarf );
                CDTXMania.tテクスチャの解放( ref this.txgbg );
                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
            if (!base.b活性化してない)
            {
                //int num9;
                if (base.b初めての進行描画)
                {
                    this.ct本体移動 = new CCounter(0, 0x1a, 20, CDTXMania.Timer);
                    this.ct本体振動 = new CCounter(0, 360, 4, CDTXMania.Timer);
                    base.b初めての進行描画 = false;
                }
                this.ct本体移動.t進行Loop();
                this.ct本体振動.t進行Loop();

                if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                {
                    #region [ ギターのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Guitar)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 65, 0, new Rectangle(0, 0, 354, 42));
                        if (base.dbゲージ値 == 1.0)
                        {
                            base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                            base.txgbarf.t2D描画(CDTXMania.app.Device, 83, 8, new Rectangle(0, 0, 318, 26));
                        }
                        else if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 83, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbg.t2D描画(CDTXMania.app.Device, 65, 0, new Rectangle(0, 42, 354, 42));
                    }
                    #endregion

                    #region [ ベースのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Bass)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 859, 0, new Rectangle(0, 0, 354, 42));
                        if (base.dbゲージ値 == 1.0)
                        {
                            base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                            base.txgbarf.t2D描画(CDTXMania.app.Device, 877, 8, new Rectangle(0, 0, 318, 26));
                        }
                        else if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 877, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbg.t2D描画(CDTXMania.app.Device, 859, 0, new Rectangle(0, 42, 354, 42));
                    }
                    #endregion
                }
                else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                {
                    #region [ ギターのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Guitar)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 0, 354, 42));
                        if (base.dbゲージ値 == 1.0)
                        {
                            base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                            base.txgbarf.t2D描画(CDTXMania.app.Device, 18, 8, new Rectangle(0, 0, 318, 26));
                        }
                        else if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 18, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbg.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 42, 354, 42));
                    }
                    #endregion
                    #region [ ベースのゲージ ]
                    if (base.txgbg != null && CDTXMania.DTX.bチップがある.Bass)
                    {
                        base.txgbg.t2D描画(CDTXMania.app.Device, 934, 0, new Rectangle(0, 0, 354, 42));
                        if (base.dbゲージ値 == 1.0)
                        {
                            base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                            base.txgbarf.t2D描画(CDTXMania.app.Device, 940 + 13, 8, new Rectangle(0, 0, 318, 26));
                        }
                        else if (base.dbゲージ値 > 0.0)
                        {
                            base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                            base.txgbar.t2D描画(CDTXMania.app.Device, 940 + 13, 8, new Rectangle(0, 0, 318, 26));
                        }
                        base.txgbg.t2D描画(CDTXMania.app.Device, 934, 0, new Rectangle(0, 42, 354, 42));
                    }
                    #endregion
                }
            }
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//private CCounter ct本体移動;
		//private CCounter ct本体振動;
		//-----------------
		#endregion
	}
}
