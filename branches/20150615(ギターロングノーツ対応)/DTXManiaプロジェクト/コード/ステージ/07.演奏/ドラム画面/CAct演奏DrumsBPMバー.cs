using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏DrumsBPMバー : CAct演奏BPMバー共通
	{
		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
            if (!base.b活性化してない)
            {
                int num1 = (int)base.ctBPMバー.db現在の値;
                if ((base.txBPMバー左 != null && base.txBPMバー右 != null) && CDTXMania.stage演奏ドラム画面.ct登場用.n現在の値 >= 11)
                {
                    if ( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A )
                    {
                        base.txBPMバー右.t2D描画(CDTXMania.app.Device, 900 + (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54);

                        if (CDTXMania.stage演奏ドラム画面.bサビ区間 && base.txBPMバーフラッシュ右 != null)
                        {
                            base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(255 * num1 / 14);
                            base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, 896 + (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54);
                        }
                    }
                    if ( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A || CDTXMania.ConfigIni.eBPMbar == Eタイプ.B )
                    {
                        base.txBPMバー左.t2D描画(CDTXMania.app.Device, 231 - (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54);

                        if (CDTXMania.stage演奏ドラム画面.bサビ区間 && base.txBPMバーフラッシュ左 != null)
                        {
                            base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(255 * num1 / 14);
                            base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 220 - (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54);
                        }
                    }
                }
            }
			return 0;
		}
	}
}
