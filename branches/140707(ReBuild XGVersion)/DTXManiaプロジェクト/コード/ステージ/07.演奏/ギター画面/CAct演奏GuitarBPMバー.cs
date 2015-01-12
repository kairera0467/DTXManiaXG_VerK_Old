using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏GuitarBPMバー : CAct演奏BPMバー共通
    {
        public override int On進行描画()
        {
            //共通数値
            int nBPMCounter = 0;
            if( base.ctBPMバー != null )
            {
                nBPMCounter = (int)base.ctBPMバー.db現在の値;
            }
            float dbBarPos = (float)( 6.0f * Math.Sin( Math.PI * nBPMCounter / 14.0f ) );

            //ギターは(XG1を除き)ベース部分が存在しないので、素材が見つかって画像が作成できるまではベース部分は封印しておく。
            if( base.txバー穴 != null )
            {
                //1P
                base.txバー穴.t2D描画( CDTXMania.app.Device, 71, 42, new Rectangle( 0, 0, 14, 627 ) );
                base.txバー穴.t2D描画( CDTXMania.app.Device, 325, 42, new Rectangle( 14, 0, 14, 627 ) );

                //2P
                base.txバー穴.t2D描画( CDTXMania.app.Device, 941, 44, new Rectangle( 0, 0, 14, 627 ) );
                base.txバー穴.t2D描画( CDTXMania.app.Device, 1195, 44, new Rectangle( 14, 0, 14, 627 ) );
            }


            //BPMバー Height 10
            if( base.txバー本体 != null )
            {
                //1P
                base.txバー本体.t2D描画( CDTXMania.app.Device, 61 - dbBarPos, 54, new Rectangle( 28, 0, 10, 600 ) );
                base.txバー本体.t2D描画( CDTXMania.app.Device, 331 + dbBarPos, 54, new Rectangle( 38, 0, 10, 600 ) );

                //2P
                base.txバー本体.t2D描画( CDTXMania.app.Device, 931 - dbBarPos, 54, new Rectangle( 28, 0, 10, 600 ) );
                base.txバー本体.t2D描画( CDTXMania.app.Device, 1201 + dbBarPos, 54, new Rectangle( 38, 0, 10, 600 ) );
            }

            //Flush Width 32  L 48  R 80
            if( base.txバー本体 != null && base.bサビ区間中 == true )
            {
                base.txバーサビ.n透明度 = 255 - (int)(255 * nBPMCounter / 14);

                //1P
                base.txバーサビ.t2D描画( CDTXMania.app.Device, 48 - dbBarPos, 54, new Rectangle( 48, 0, 32, 600 ) );
                base.txバーサビ.t2D描画( CDTXMania.app.Device, 896 + dbBarPos, 54, new Rectangle( 80, 0, 32, 600 ) );

                //2P
                base.txバーサビ.t2D描画( CDTXMania.app.Device, 931 - dbBarPos, 54, new Rectangle( 48, 0, 32, 600 ) );
                base.txバーサビ.t2D描画( CDTXMania.app.Device, 918 + dbBarPos, 54, new Rectangle( 80, 0, 32, 600 ) );
            }

            return base.On進行描画();
        }
    }
}
