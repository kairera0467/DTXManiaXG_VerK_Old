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

                #region [ スコアを桁数ごとに n位の数[] に格納する。CAct演奏コンボ共通の使いまわし。 ]
			    //-----------------
			    //座標側の管理が複雑になるので、コンボとは違い、右から数値を入れていく。
                int n = (int)this.n現在表示中のスコア.Drums;
			    int n桁数 = 0;
                int[] n位の数 = new int[ ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 7 ) ];
			    while( ( n > 0 ) && ( n桁数 < ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 7 ) ) )
			    {
			    	n位の数[ ( CDTXMania.ConfigIni.nSkillMode == 0 ? 9 : 6 ) - n桁数 ] = n % 10;
			    	n = ( n - ( n % ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 10 ) ) ) / ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 10 );
			    	n桁数++;
			    }

                int n2 = (int)this.n現在の本当のスコア.Drums;
                int n桁数2 = 0;
                int[] n位の数2 = new int[ ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 7 ) ];
                while ((n2 > 0) && (n桁数2 < ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 7 )))
                {
                    n位の数2[ ( CDTXMania.ConfigIni.nSkillMode == 0 ? 9 : 6 ) - n桁数2 ] = n2 % 10;
                    n2 = (n2 - (n2 % ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 10 ))) / ( CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 10 );
                    n桁数2++;
                }
			    //-----------------
			    #endregion

                if (num < base.n進行用タイマ)
                {
                    base.n進行用タイマ = num;
                }
                while ((num - base.n進行用タイマ) >= 15)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.n現在表示中のスコア[j] += this.nスコアの増分[j];

                        if (this.n現在表示中のスコア[j] > (long)this.n現在の本当のスコア[j])
                            this.n現在表示中のスコア[j] = (long)this.n現在の本当のスコア[j];
                    }
                    base.n進行用タイマ += 15;
                }
                for (int s = 0; s < (CDTXMania.ConfigIni.nSkillMode == 0 ? 10 : 7); s++)
                {
                    if (n位の数[s] == n位の数2[s])
                    {
                        base.x位置[s].Drums = 0;
                    }
                    else
                    {
                        base.x位置[s].Drums = 4;
                    }
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
                }
            }
            return 0;
        }
    }
}
