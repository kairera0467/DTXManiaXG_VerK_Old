﻿using System;
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
                if( base.txBPMバー != null )
                {
                    base.txBPMバー.n透明度 = 255;
                    //ベース
                    base.txBPMバー.t2D描画( CDTXMania.app.Device, 233, 0, new Rectangle( 176, 0, 64, 720 ) );
                    base.txBPMバー.t2D描画( CDTXMania.app.Device, 852, 0, new Rectangle( 112, 0, 64, 720 ) );

                    //穴
                    base.txBPMバー.t2D描画( CDTXMania.app.Device, 244, 44, new Rectangle( 0, 0, 14, 627 ) );
                    base.txBPMバー.t2D描画( CDTXMania.app.Device, 891, 44, new Rectangle( 14, 0, 14, 627 ) );
                }



                int num1 = (int)base.ctBPMバー.db現在の値;
                //num1 = 0;
                if( ( base.txBPMバー != null ) && CDTXMania.stage演奏ドラム画面.ct登場用.n現在の値 >= 11 )
                {
                    if( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A )
                    {
                        base.txBPMバー.n透明度 = 255;
                        base.txBPMバー.t2D描画(CDTXMania.app.Device, 900 + (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54, new Rectangle( 38, 0, 10, 600 ));

                        if( CDTXMania.stage演奏ドラム画面.bサビ区間 )
                        {
                            base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, 896 + (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54, new Rectangle( 80, 0, 32, 600 ));
                        }
                    }
                    if( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A || CDTXMania.ConfigIni.eBPMbar == Eタイプ.B )
                    {
                        base.txBPMバー.n透明度 = 255;
                        base.txBPMバー.t2D描画(CDTXMania.app.Device, 240 - (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54, new Rectangle( 28, 0, 10, 600 ));

                        if( CDTXMania.stage演奏ドラム画面.bサビ区間 )
                        {
                            base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, 220 - (float)(6 * Math.Sin(Math.PI * num1 / 14)), 54, new Rectangle( 48, 0, 32, 600 ));
                        }
                    }
                }
            }
			return 0;
		}
	}
}
