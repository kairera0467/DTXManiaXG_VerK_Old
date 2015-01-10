﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsBPMバー : CAct演奏BPMバー共通
    {
        public override int On進行描画()
        {
            if( base.txバー下地 != null )
                base.txバー下地.t2D描画( CDTXMania.app.Device, 0, 0 );
            if( base.txバー穴 != null )
            {
                base.txバー穴.t2D描画( CDTXMania.app.Device, 244, 44, new Rectangle( 0, 0, 14, 627 ) );
                base.txバー穴.t2D描画( CDTXMania.app.Device, 891, 44, new Rectangle( 14, 0, 14, 627 ) );
            }

            int nBPMCounter = 0;
            if( base.ctBPMバー != null )
            {
                nBPMCounter = (int)base.ctBPMバー.db現在の値;
            }
            float dbBarPos = (float)( 6.0f * Math.Sin( Math.PI * nBPMCounter / 14.0f ) );

            //BPMバー Height 10
            if( base.txバー本体 != null )
            {
                //左
                base.txバー本体.t2D描画( CDTXMania.app.Device, 240 - dbBarPos, 54, new Rectangle( 28, 0, 10, 600 ) );

                //右
                base.txバー本体.t2D描画( CDTXMania.app.Device, 900 + dbBarPos, 54, new Rectangle( 38, 0, 10, 600 ) );

            }

            //Flush Height 32  L 48  R 80

            return base.On進行描画();
        }
    }
}
