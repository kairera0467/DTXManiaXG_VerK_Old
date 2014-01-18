using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏GuitarLivePoint : CAct演奏LivePoint共通
    {
        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {

            int n本体ギターX = 0;
            int n本体ギターY = 52;

            int n本体ベースX = 1209;
            int n本体ベースY = 52;


            if (!base.b活性化してない)
            {
                if (base.b初めての進行描画)
                {
                    base.b初めての進行描画 = false;
                }

                #region[Guitar]
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (this.txLivePointゲージ != null)
                        this.txLivePointゲージ.t2D描画(CDTXMania.app.Device, n本体ギターX, n本体ギターY, new Rectangle(0, 0, 71, 668));

                    string strG = string.Format("{0,3:##0}", base.n現在のLivePoint.Guitar);
                    for (int i = 0; i < 3; i++)
                    {
                        Rectangle rectangle;
                        char ch = strG[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(72, 250, 17, 25);
                        }
                        else
                        {
                            int num3 = int.Parse(strG.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                            }
                            else
                            {
                                rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                            }
                        }
                        if (base.txLivePoint != null)
                        {
                            this.txLivePoint.t2D描画(CDTXMania.app.Device, 9 + n本体ギターX + (i * 18), 633 + n本体ギターY, rectangle);
                        }
                    }
                    #region[箱]
                    //まず箱を再現するためにはLPが一定以上になったら表示させるような仕掛けが必要。
                    if (base.n現在のLivePoint.Guitar >= 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (this.n現在のLivePoint.Guitar >= 20 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ギターX + 1, 574 + n本体ギターY - (41 * i), new Rectangle(71, 536, 46, 44));
                            }
                            if (this.n現在のLivePoint.Guitar >= 120 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ギターX + 1, 368 + n本体ギターY - (41 * i), new Rectangle(71, 580, 46, 44));
                            }
                            if (this.n現在のLivePoint.Guitar >= 220 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ギターX + 1, 162 + n本体ギターY - (41 * i), new Rectangle(71, 624, 46, 44));
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                #region[Bass]
                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (this.txLivePointゲージ != null)
                        this.txLivePointゲージ.t2D描画(CDTXMania.app.Device, n本体ベースX, n本体ベースY, new Rectangle(0, 0, 71, 668));

                    string strB = string.Format("{0,3:##0}", base.n現在のLivePoint.Bass);
                    for (int i = 0; i < 3; i++)
                    {
                        Rectangle rectangle;
                        char ch = strB[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(72, 250, 17, 25);
                        }
                        else
                        {
                            int num3 = int.Parse(strB.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                            }
                            else
                            {
                                rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                            }
                        }
                        if (base.txLivePoint != null)
                        {
                            this.txLivePoint.t2D描画(CDTXMania.app.Device, 9 + n本体ベースX + (i * 18), 633 + n本体ベースY, rectangle);
                        }
                    }
                    #region[箱]
                    //まず箱を再現するためにはLPが一定以上になったら表示させるような仕掛けが必要。
                    if (base.n現在のLivePoint.Guitar >= 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (this.n現在のLivePoint.Bass >= 20 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ベースX + 1, 574 + n本体ベースY - (41 * i), new Rectangle(71, 536, 46, 44));
                            }
                            if (this.n現在のLivePoint.Bass >= 120 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ベースX + 1, 368 + n本体ベースY - (41 * i), new Rectangle(71, 580, 46, 44));
                            }
                            if (this.n現在のLivePoint.Bass >= 220 + (20 * i))
                            {
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体ベースX + 1, 162 + n本体ベースY - (41 * i), new Rectangle(71, 624, 46, 44));
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            return 0;
        }
    }
}
