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

                int nギター左X = 60;
                int nギター右X = 331;
                int nベース左X = 930;
                int nベース右X = 1200;
                int nバーY = 54;

                if ((base.txBPMバー左 != null && base.txBPMバー右 != null))// && CDTXMania.stage演奏ギター画面.ct登場用.n現在の値 >= 11)
                {
                    if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.A)
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, nギター左X, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, nギター右X, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間)
                            {
                                if (base.txBPMバーフラッシュ右 != null && base.txBPMバーフラッシュ左 != null)
                                {
                                    base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 149 + nギター左X + (1 * num1), nバーY);

                                    base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, nギター右X - (1 * num1), nバーY);
                                }
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, nベース左X, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, nベース右X, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間)
                            {
                                if (base.txBPMバーフラッシュ右 != null && base.txBPMバーフラッシュ左 != null)
                                {
                                    base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 149 + nベース左X + (1 * num1), nバーY);

                                    base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, nベース右X - (1 * num1), nバーY);
                                }
                            }
                        }

                    }
                    else if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.B)
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー右.t2D描画(CDTXMania.app.Device, 331, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ右 != null)
                            {
                                base.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, nギター右X - (1 * num1), nバーY);
                            }
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー左.t2D描画(CDTXMania.app.Device, nベース左X, nバーY, new Rectangle(0, 0 + (600 * num1), 19, 600));

                            if (CDTXMania.stage演奏ギター画面.bサビ区間 && base.txBPMバーフラッシュ左 != null)
                            {
                                base.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                base.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 149 + nベース左X + (1 * num1), nバーY);
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
