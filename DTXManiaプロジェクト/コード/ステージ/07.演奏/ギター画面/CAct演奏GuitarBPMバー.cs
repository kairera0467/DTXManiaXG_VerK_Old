using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
    internal class CAct演奏GuitarBPMバー : CAct演奏BPMバー共通
    {
        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                int num1 = (int)base.ctBPMバー.db現在の値;

                float fギター左X = 61;
                float fギター右X = 331;
                float fベース左X = 931;
                float fベース右X = 1201;
                float fバーY = 54;

                if ((base.txBPMバー左 != null && base.txBPMバー右 != null))// && CDTXMania.stage演奏ギター画面.ct登場用.n現在の値 >= 11)
                {
                    if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.A)
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, fギター左X - (float)(9 * Math.Sin(Math.PI * num1 / 14)), fバーY);

                            if ( CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ左 != null )
                            {
                                base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 149 + fギター左X + (1 * num1), fバーY);
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, fベース右X + (float)(9 * Math.Sin(Math.PI * num1 / 14)), fバーY);

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ右 != null)
                            {
                                base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, fベース右X - (1 * num1), fバーY);

                            }
                        }

                    }
                    if ( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A || CDTXMania.ConfigIni.eBPMbar == Eタイプ.B )
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, fギター右X + (float)(9 * Math.Sin(Math.PI * num1 / 14)), fバーY);

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ右 != null)
                            {
                                base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, fギター右X - (1 * num1), fバーY);
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, fベース左X - (float)(9 * Math.Sin(Math.PI * num1 / 14)), fバーY);

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ左 != null)
                            {
                                base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 149 + fベース左X + (1 * num1), fバーY);
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
