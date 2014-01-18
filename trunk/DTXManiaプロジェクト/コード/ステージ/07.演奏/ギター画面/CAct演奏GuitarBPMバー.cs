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
                if ((base.txBPMバー左 != null && base.txBPMバー右 != null))// && CDTXMania.stage演奏ギター画面.ct登場用.n現在の値 >= 11)
                {
                    if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.A)
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, 60, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, 331, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間)
                            {
                                if (base.txBPMバーフラッシュ右 != null && base.txBPMバーフラッシュ左 != null)
                                {
                                    base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 209 + (1 * num1), 56);

                                    base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, 331 - (1 * num1), 56);
                                }
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, 930, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, 1200, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間)
                            {
                                if (base.txBPMバーフラッシュ右 != null && base.txBPMバーフラッシュ左 != null)
                                {
                                    base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 1079 + (1 * num1), 56);

                                    base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, 1201 - (1 * num1), 56);
                                }
                            }
                        }

                    }
                    else if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.B)
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, 331, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ右 != null)
                            {
                                base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, 331 - (1 * num1), 56);
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, 930, 56, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ左 != null)
                            {
                                base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 1079 + (1 * num1), 56);
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
