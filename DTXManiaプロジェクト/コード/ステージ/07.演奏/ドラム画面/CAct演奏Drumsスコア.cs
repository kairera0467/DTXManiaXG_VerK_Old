using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsスコア : CAct演奏スコア共通
    {
        // CActivity 実装（共通クラスからの差分のみ）

        public override unsafe int On進行描画()
        {
            if (!base.b活性化してない)
            {
                if (base.b初めての進行描画)
                {
                    base.n進行用タイマ = CDTXMania.Timer.n現在時刻;
                    base.b初めての進行描画 = false;
                }
                long num = CDTXMania.Timer.n現在時刻;
                if (num < base.n進行用タイマ)
                {
                    base.n進行用タイマ = num;
                }
                while ((num - base.n進行用タイマ) >= 10)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.n現在表示中のスコア[j] += this.nスコアの増分[j];

                        if (this.n現在表示中のスコア[j] > (long)this.n現在の本当のスコア[j])
                            this.n現在表示中のスコア[j] = (long)this.n現在の本当のスコア[j];
                    }
                    base.n進行用タイマ += 10;
                }
                string str = this.n現在表示中のスコア.Drums.ToString("0000000");
                //string str = CDTXMania.stage演奏ドラム画面.actAVI.LivePoint.ToString("0000000");
                for (int i = 0; i < 7; i++)
                {
                    Rectangle rectangle;
                    char ch = str[i];
                    if (ch.Equals(' '))
                    {
                        rectangle = new Rectangle(0, 0, 36, 50);
                    }
                    else
                    {
                        int num4 = int.Parse(str.Substring(i, 1));
                        rectangle = new Rectangle(num4 * 36, 0, 36, 50);
                    }
                    if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
                    {
                        if (base.txScore != null)
                        {

                            base.txScore.t2D描画(CDTXMania.app.Device, 30 + (i * 34), 40, rectangle);
                        }
                    }
                } if (base.txScore != null)
                {
                    base.txScore.t2D描画(CDTXMania.app.Device, 30, 12, new Rectangle(0, 50, 86, 28));
                }
            }
            return 0;
        }
    }
}
