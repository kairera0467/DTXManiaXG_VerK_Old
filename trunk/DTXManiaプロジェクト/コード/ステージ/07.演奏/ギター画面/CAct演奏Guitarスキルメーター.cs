using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Guitarスキルメーター : CActivity
    {


        // #24074 2011.01.23 ikanick グラフの描画
        // 実装内容
        // ・左を現在、右を目標
        // ・基準線(60,70,80,90,100%)を超えると線が黄色くなる（元は白）
        // ・目標を超えると現在が光る
        // ・オート時には描画しない
        // 要望・実装予定
        // ・グラフを波打たせるなどの視覚の向上→実装済
        // 修正等
        // ・画像がないと落ちる→修正済

        // プロパティ

        public double dbグラフ値現在G_渡
        {
            get
            {
                return this.dbグラフ値現在;
            }
            set
            {
                this.dbグラフ値現在 = value;
            }
        }
        public double dbグラフ値ゴーストG_渡 //2015.12.31 kairera0467 XG2を見る限り、ゴースト数値はゲージ画像と比較数値にしか使用されていないので、ゴースト数値と目標値は別に扱うことにした。
        {
            get
            {
                return this.dbグラフ値ゴースト;
            }
            set
            {
                this.dbグラフ値ゴースト = value;
            }
        }
        public double dbグラフ値目標G_渡
        {
            get
            {
                return this.dbグラフ値目標;
            }
            set
            {
                this.dbグラフ値目標 = value;
            }
        }

        public double dbグラフ値現在B_渡
        {
            get
            {
                return this.dbグラフ値現在;
            }
            set
            {
                this.dbグラフ値現在 = value;
            }
        }
        public double dbグラフ値ゴーストB_渡 //2015.12.31 kairera0467 XG2を見る限り、ゴースト数値はゲージ画像と比較数値にしか使用されていないので、ゴースト数値と目標値は別に扱うことにした。
        {
            get
            {
                return this.dbグラフ値ゴースト;
            }
            set
            {
                this.dbグラフ値ゴースト = value;
            }
        }
        public double dbグラフ値目標B_渡
        {
            get
            {
                return this.dbグラフ値目標;
            }
            set
            {
                this.dbグラフ値目標 = value;
            }
        }

        // コンストラクタ

        public CAct演奏Guitarスキルメーター()
        {
            ST文字位置[] st文字位置Array = new ST文字位置[11];
            ST文字位置 st文字位置 = new ST文字位置();
            st文字位置.ch = '0';
            st文字位置.pt = new Point(288, 0);
            st文字位置Array[0] = st文字位置;
            ST文字位置 st文字位置2 = new ST文字位置();
            st文字位置2.ch = '1';
            st文字位置2.pt = new Point(300, 0);
            st文字位置Array[1] = st文字位置2;
            ST文字位置 st文字位置3 = new ST文字位置();
            st文字位置3.ch = '2';
            st文字位置3.pt = new Point(312, 0);
            st文字位置Array[2] = st文字位置3;
            ST文字位置 st文字位置4 = new ST文字位置();
            st文字位置4.ch = '3';
            st文字位置4.pt = new Point(324, 0);
            st文字位置Array[3] = st文字位置4;
            ST文字位置 st文字位置5 = new ST文字位置();
            st文字位置5.ch = '4';
            st文字位置5.pt = new Point(336, 0);
            st文字位置Array[4] = st文字位置5;
            ST文字位置 st文字位置6 = new ST文字位置();
            st文字位置6.ch = '5';
            st文字位置6.pt = new Point(348, 0);
            st文字位置Array[5] = st文字位置6;
            ST文字位置 st文字位置7 = new ST文字位置();
            st文字位置7.ch = '6';
            st文字位置7.pt = new Point(360, 0);
            st文字位置Array[6] = st文字位置7;
            ST文字位置 st文字位置8 = new ST文字位置();
            st文字位置8.ch = '7';
            st文字位置8.pt = new Point(372, 0);
            st文字位置Array[7] = st文字位置8;
            ST文字位置 st文字位置9 = new ST文字位置();
            st文字位置9.ch = '8';
            st文字位置9.pt = new Point(384, 0);
            st文字位置Array[8] = st文字位置9;
            ST文字位置 st文字位置10 = new ST文字位置();
            st文字位置10.ch = '9';
            st文字位置10.pt = new Point(396, 0);
            st文字位置Array[9] = st文字位置10;
            ST文字位置 st文字位置11 = new ST文字位置();
            st文字位置11.ch = '.';
            st文字位置11.pt = new Point(408, 0);
            st文字位置Array[10] = st文字位置11;
            this.st小文字位置 = st文字位置Array;
            base.b活性化してない = true;
        }


        // CActivity 実装

        public override void On活性化()
        {
            this.n本体X[0] = 966;
            this.n本体X[1] = 356;
            this.n本体X[2] = 640;

            this.dbグラフ値目標 = 80f;
            this.dbグラフ値現在 = 0f;
            this.dbグラフ値比較 = 0f;
            this.db現在の判定数合計 = 0f;
            base.On活性化();
        }
        public override void On非活性化()
        {
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 16, FontStyle.Bold );

                this.txグラフ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Graph_main.png" ) );
                this.tx比較 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Graph_main.png" ) );
                this.txグラフバックパネル = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\7_Graph_main.png" ) );
                this.txグラフゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Graph_Gauge.png" ) );

                if( this.pfNameFont != null )
                {
                    if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT || CDTXMania.ConfigIni.eTargetGhost.Guitar == ETargetGhostData.PERFECT || CDTXMania.ConfigIni.eTargetGhost.Bass == ETargetGhostData.PERFECT )
                    {
                        this.txPlayerName = this.t指定された文字テクスチャを生成する( "DJ AUTO" );
                    }
                    else if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY || CDTXMania.ConfigIni.eTargetGhost.Guitar == ETargetGhostData.LAST_PLAY || CDTXMania.ConfigIni.eTargetGhost.Bass == ETargetGhostData.LAST_PLAY )
                    {
                        this.txPlayerName = this.t指定された文字テクスチャを生成する( "LAST PLAY" );
                    }
                }
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放( ref this.txグラフ );
                CDTXMania.tテクスチャの解放( ref this.tx比較 );
                CDTXMania.tテクスチャの解放( ref this.txグラフバックパネル );
                CDTXMania.tテクスチャの解放( ref this.txPlayerName );
                base.OnManagedリソースの解放();
            }
        }
        public override int On進行描画()
        {
            int j = 0;

            if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (!CDTXMania.DTX.bチップがある.Bass)
                    j = 1;
                else if (!CDTXMania.DTX.bチップがある.Guitar)
                    j = 2;
                else if (!CDTXMania.ConfigIni.bギターが全部オートプレイである && CDTXMania.ConfigIni.bベースが全部オートプレイである)
                    j = 1;
                else if (CDTXMania.ConfigIni.bギターが全部オートプレイである && !CDTXMania.ConfigIni.bベースが全部オートプレイである)
                    j = 2;
            }

            if (!base.b活性化してない)
            {
                if (base.b初めての進行描画)
                {
                    this.ct爆発エフェクト = new CCounter(0, 13, 20, CDTXMania.Timer);
                    base.b初めての進行描画 = false;
                }
                double db1ノーツごとの達成率 = (double)this.dbグラフ値目標 / CDTXMania.DTX.n可視チップ数[j];

                if (j == 0)
                    this.n現在演奏されたノーツ数 =
                        CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む[j].Perfect +
                        CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む[j].Great +
                        CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む[j].Good +
                        CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む[j].Poor +
                        CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む[j].Miss;
                else
                    this.n現在演奏されたノーツ数 =
                        CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[j].Perfect +
                        CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[j].Great +
                        CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[j].Good +
                        CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[j].Poor +
                        CDTXMania.stage演奏ギター画面.nヒット数・Auto含む[j].Miss;

                CScoreIni.C演奏記録 drums = new CScoreIni.C演奏記録();

                double rate = (double)n現在演奏されたノーツ数 / (double)CDTXMania.DTX.n可視チップ数[j];

                if (CDTXMania.ConfigIni.nSkillMode == 0)
                {
                    //int n逆算Perfect = drums.nPerfect数・Auto含まない / this.n現在演奏されたノーツ数;
                    //int n逆算Great = drums.nGreat数・Auto含まない / this.n現在演奏されたノーツ数;
                    //this.dbグラフ値比較 = CScoreIni.t旧ゴーストスキルを計算して返す(CDTXMania.DTX.n可視チップ数[j], drums.nPerfect数, drums.nGreat数, drums.nGood数, drums.nPoor数, drums.nMiss数, E楽器パート[j]) * rate;
                    //this.dbグラフ値比較 = ((this.dbグラフ値目標_渡) / (double)CDTXMania.DTX.n可視チップ数[j]) * rate;
                    this.dbグラフ値比較 = this.dbグラフ値目標G_渡;
                }
                else if (CDTXMania.ConfigIni.nSkillMode == 1)
                {
                    E楽器パート ePart = E楽器パート.UNKNOWN;
                    switch( j )
                    {
                        case 0:
                            ePart = E楽器パート.DRUMS;
                            break;
                        case 1:
                            ePart = E楽器パート.GUITAR;
                            break;
                        case 2:
                            ePart = E楽器パート.BASS;
                            break;
                    }


                    this.dbグラフ値比較 = CScoreIni.tゴーストスキルを計算して返す(CDTXMania.DTX.n可視チップ数[j], this.n現在演奏されたノーツ数, drums.n最大コンボ数, ePart );
                    //this.dbグラフ値比較 = (double)(db1ノーツごとの達成率 * n現在演奏されたノーツ数);
                }


                //this.dbグラフ値比較 = (double)(db1ノーツごとの達成率 * n現在演奏されたノーツ数);
                // 背景暗幕
                Rectangle rectangle = new Rectangle( 2, 2, 280, 720 );
                if (this.txグラフ != null)
                {
                    this.txグラフバックパネル.t2D描画( CDTXMania.app.Device, this.n本体X[j], 50, rectangle );
                    //this.txグラフバックパネル.t2D描画( CDTXMania.app.Device, 141 + this.n本体X[j], 650 - (int)(this.dbグラフ値現在 * 5.56), new Rectangle(499, 0, 201, (int)(this.dbグラフ値現在 * 5.56)));
                }



                // 基準線
                rectangle = new Rectangle(78, 0, 60, 3);

                for (int i = 0; i < 5; i++)
                {
                    // 基準線を越えたら線が黄色くなる
                    if (this.dbグラフ値現在 >= (100 - i * 10))
                    {
                        rectangle = new Rectangle(78, 1, 60, 2);//黄色
                        if (this.txグラフ != null)
                        {
                            //this.txグラフ.n透明度 = 224;
                        }
                    }
                    else
                    {
                        rectangle = new Rectangle(420, 566, 60, 2);
                        if (this.txグラフ != null)
                        {
                            this.txグラフ.n透明度 = 160;
                        }
                    }

                    if (this.txグラフ != null)
                    {
                        this.txグラフ.t2D描画( CDTXMania.app.Device, 8 + this.n本体X[j], 94 + (int)(58.52 * i), rectangle );
                    }
                }
                // グラフ
                // --現在値
                if (this.dbグラフ値現在_表示 < this.dbグラフ値現在)
                {
                    this.dbグラフ値現在_表示 += (this.dbグラフ値現在 - this.dbグラフ値現在_表示) / 5 + 0.01;
                }
                if (this.dbグラフ値現在_表示 >= this.dbグラフ値現在)
                {
                    this.dbグラフ値現在_表示 = this.dbグラフ値現在;
                }
                rectangle = new Rectangle(288, 2, 72, (int)(556f * this.dbグラフ値現在_表示 / 100));

                if( this.txグラフゲージ != null )
                {
                    this.txグラフゲージ.vc拡大縮小倍率 = new Vector3(1f, 1f, 1f);
                    this.txグラフゲージ.n透明度 = 255;
                    this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 3 + this.n本体X[ j ], 650 - ( int )( 556f * this.dbグラフ値現在_表示 / 100.0 ), rectangle );
                }

                this.dbグラフ値直前 = this.dbグラフ値現在;

                // --現在値_目標越
                rectangle = new Rectangle(0, 0, 210, (int)(556f * this.dbグラフ値現在_表示 / 100));
                if ((dbグラフ値現在 >= dbグラフ値目標) && (this.txグラフゲージ != null))
                {
                    //this.txグラフ.vc拡大縮小倍率 = new Vector3(1.4f, 1f, 1f);
                    //this.txグラフ.n透明度 = 128;
                    //this.txグラフ.b加算合成 = true;
                    this.txグラフゲージ.n透明度 = 255;
                    this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 74 + this.n本体X[j], 650 - (int)( 556f * this.dbグラフ値現在_表示 / 100 ), rectangle);

                }
                // --目標値

                if (this.dbグラフ値目標_表示 < this.dbグラフ値目標)
                {
                    this.dbグラフ値目標_表示 += (this.dbグラフ値目標 - this.dbグラフ値目標_表示) / 5 + 0.01;
                }
                if (this.dbグラフ値目標_表示 >= this.dbグラフ値目標)
                {
                    this.dbグラフ値目標_表示 = this.dbグラフ値目標;
                }

                db現在の判定数合計 = 0;
                //db現在の判定数合計 = CDTXMania.stage演奏画面共通.nヒット数・Auto含む[j].Perfect + CDTXMania.stage演奏画面共通.nヒット数・Auto含む[j].Great + CDTXMania.stage演奏画面共通.nヒット数・Auto含む[j].Good + CDTXMania.stage演奏画面共通.nヒット数・Auto含む[j].Miss + CDTXMania.stage演奏画面共通.nヒット数・Auto含む[j].Poor;
                //this.dbグラフ値目標_Ghost = ((1.0 * CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[0] / CDTXMania.DTX.n可視チップ数[j]) * db現在の判定数合計);
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"debug.txt", true, System.Text.Encoding.GetEncoding("shift_jis"));
                //sw.WriteLine("TotalJudgeは{0}で、Ghostは{1}です。", db現在の判定数合計, this.dbグラフ値目標_Ghost);
                //sw.Close();
                this.dbグラフ値ゴースト_表示 = this.dbグラフ値ゴースト;
                rectangle = new Rectangle(138, 0, 72, (int)(556.0 * this.dbグラフ値ゴースト_表示 / 100.0));
                if (this.txグラフゲージ != null)
                {
                    //this.txグラフゲージ.n透明度 = 48;
                    this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 75 + this.n本体X[j], 650 - (int)(556.0 * this.dbグラフ値ゴースト_表示 / 100.0), new Rectangle( 2, 0, 201, (int)( 556.0 * this.dbグラフ値ゴースト_表示 / 100.0 ) ) );
                }


                this.t小文字表示(204 + this.n本体X[j], 658, string.Format("{0,6:##0.00}%", this.dbグラフ値現在));
                if( CDTXMania.ConfigIni.nInfoType == 0 )
                {
                    if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT || CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
                    {
                        this.tx比較.t2D描画( CDTXMania.app.Device, 102 + this.n本体X[ j ], 200, new Rectangle( 288, 2, 162, 85 ) );
                        if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
                            this.t小文字表示( 192 + this.n本体X[ j ], 250, string.Format( "{0,6:##0.00}%", this.dbグラフ値ゴースト ) );
                        else
                            this.t小文字表示( 192 + this.n本体X[ j ], 250, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標 ) );
                        if( this.dbグラフ値現在 > this.dbグラフ値目標 )
                        {
                            this.tx比較.n透明度 = 128;
                        }
                        if( this.txPlayerName != null )
                        {
                            this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + this.n本体X[ j ], 214 );
                        }
                    }
                    else
                    {
                        this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 200, new Rectangle(288, 160, 162, 60));
                        this.t小文字表示(186 + this.n本体X[j], 224, string.Format("{0,6:##0.00}%", this.dbグラフ値目標));
                        if (this.dbグラフ値現在 > this.dbグラフ値目標)
                        {
                            this.tx比較.n透明度 = 128;
                        }
                    }
                }
                else if( CDTXMania.ConfigIni.nInfoType == 1 )
                {
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 200, new Rectangle(288, 226, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 280, new Rectangle(288, 292, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 360, new Rectangle(288, 358, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 440, new Rectangle(288, 424, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X[j], 520, new Rectangle(288, 490, 162, 60));

                    if (j == 0)
                    {
                        this.t小文字表示(186 + this.n本体X[j], 224, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない[j].Perfect));
                        this.t小文字表示(186 + this.n本体X[j], 304, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない[j].Great));
                        this.t小文字表示(186 + this.n本体X[j], 384, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない[j].Good));
                        this.t小文字表示(186 + this.n本体X[j], 464, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない[j].Poor));
                        this.t小文字表示(186 + this.n本体X[j], 544, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない[j].Miss));
                    }
                    else
                    {
                        this.t小文字表示(186 + this.n本体X[j], 224, string.Format("{0,6:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[j].Perfect));
                        this.t小文字表示(186 + this.n本体X[j], 304, string.Format("{0,6:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[j].Great));
                        this.t小文字表示(186 + this.n本体X[j], 384, string.Format("{0,6:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[j].Good));
                        this.t小文字表示(186 + this.n本体X[j], 464, string.Format("{0,6:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[j].Poor));
                        this.t小文字表示(186 + this.n本体X[j], 544, string.Format("{0,6:###0}", CDTXMania.stage演奏ギター画面.nヒット数・Auto含まない[j].Miss));
                    }
                }
            }
            return 0;
        }


        // その他

        #region [ private ]
        //----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct STキラキラ
        {
            public int x;
            public int y;
            public float fScale;
            public int Trans;
            public CCounter ct進行;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
        }
        private readonly ST文字位置[] st小文字位置;
        private STキラキラ[] stキラキラ = new STキラキラ[64];
        private STキラキラ[] stフラッシュ = new STキラキラ[16];

        private CCounter ct爆発エフェクト;
        public double db現在の判定数合計;
        private double dbグラフ値目標;
        private double dbグラフ値ゴースト;
        private double dbグラフ値ゴースト_表示;
        public double dbグラフ値比較;
        private double dbグラフ値目標_表示;
        private double dbグラフ値現在;
        private double dbグラフ値現在_表示;
        private double dbグラフ値直前;
        private int nグラフフラッシュct;
        private int n現在演奏されたノーツ数;
        private STDGBVALUE<int> n本体X;
        private CTexture tx比較;
        private CTexture txグラフ;
        private CTexture txグラフバックパネル;
        private CTexture txグラフゲージ;
        private CTexture txPlayerName;

        private Bitmap bmpGraph;
        private Graphics gGraph;
        private CPrivateFastFont pfNameFont;
        //-----------------

        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            //2015.12.27.kairera0467 こいついつも使いまわししてんな。
            //まだDJ.AUTO専用なので、文字列の長さとかそういうのは考慮してません。
            Bitmap bmp;
            
            bmp = this.pfNameFont.DrawPrivateFont( str文字, Color.White, Color.Transparent );

            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );

            if( tx文字テクスチャ != null )
                tx文字テクスチャ.vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1f );

            bmp.Dispose();

            return tx文字テクスチャ;
        }

        private void t小文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st小文字位置.Length; i++)
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle( this.st小文字位置[i].pt.X, 556, 12, 16 );
                        if (ch == '.')
                        {
                            rectangle.Width -= 8;
                        }
                        if (this.txグラフバックパネル != null)
                        {
                            this.txグラフバックパネル.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }

                if( ch == '.' ) x += 6;
                else x += 12;

            }
        }
        #endregion
    }
}
