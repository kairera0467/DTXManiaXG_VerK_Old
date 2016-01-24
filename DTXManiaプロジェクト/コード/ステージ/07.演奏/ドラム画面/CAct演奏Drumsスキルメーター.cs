using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsスキルメーター : CActivity
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

        // 2016.01.10 kairera0467 再びドラム、ギターで分離

        public double dbグラフ値現在_渡
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
        public double dbグラフ値ゴースト_渡 //2015.12.31 kairera0467 XG2を見る限り、ゴースト数値はゲージ画像と比較数値にしか使用されていないので、ゴースト数値と目標値は別に扱うことにした。
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
        public double dbグラフ値目標_渡
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

        public CAct演奏Drumsスキルメーター()
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

            st文字位置Array = new ST文字位置[13];
            st文字位置.ch = '0';
            st文字位置.pt = new Point(0, 0);
            st文字位置Array[0] = st文字位置;
            st文字位置2.ch = '1';
            st文字位置2.pt = new Point(10, 0);
            st文字位置Array[1] = st文字位置2;
            st文字位置3.ch = '2';
            st文字位置3.pt = new Point(20, 0);
            st文字位置Array[2] = st文字位置3;
            st文字位置4.ch = '3';
            st文字位置4.pt = new Point(30, 0);
            st文字位置Array[3] = st文字位置4;
            st文字位置5.ch = '4';
            st文字位置5.pt = new Point(40, 0);
            st文字位置Array[4] = st文字位置5;
            st文字位置6.ch = '5';
            st文字位置6.pt = new Point(50, 0);
            st文字位置Array[5] = st文字位置6;
            st文字位置7.ch = '6';
            st文字位置7.pt = new Point(60, 0);
            st文字位置Array[6] = st文字位置7;
            st文字位置8.ch = '7';
            st文字位置8.pt = new Point(70, 0);
            st文字位置Array[7] = st文字位置8;
            st文字位置9.ch = '8';
            st文字位置9.pt = new Point(80, 0);
            st文字位置Array[8] = st文字位置9;
            st文字位置10.ch = '9';
            st文字位置10.pt = new Point(90, 0);
            st文字位置Array[9] = st文字位置10;
            st文字位置11.ch = '.';
            st文字位置11.pt = new Point(100, 0);
            st文字位置Array[10] = st文字位置11;
            ST文字位置 st文字位置12 = new ST文字位置();
            st文字位置12.ch = '-';
            st文字位置12.pt = new Point(110, 0);
            st文字位置Array[11] = st文字位置12;
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '+';
            st文字位置13.pt = new Point(120, 0);
            st文字位置Array[12] = st文字位置13;
            this.st比較数字位置 = st文字位置Array;
            base.b活性化してない = true;
        }


        // CActivity 実装

        public override void On活性化()
        {
            this.n本体X = 966;

            this.dbグラフ値目標 = 80f;
            this.dbグラフ値現在 = 0f;
            this.dbグラフ値比較 = 0f;
            this.dbグラフ値ゴースト = 0f;
            this.db現在の判定数合計 = 0f;

            this.dbグラフ値現在_表示 = 0f;
            this.dbグラフ値目標_表示 = 0f;
            this.dbグラフ値ゴースト_表示 = 0f;
            this.bUseGhost = false;
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
                    if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT )
                    {
                        this.txPlayerName = this.t指定された文字テクスチャを生成する( "DJ AUTO" );
                    }
                    else if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
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
            if (!base.b活性化してない)
            {
                if (base.b初めての進行描画)
                {
                    this.ct爆発エフェクト = new CCounter(0, 13, 20, CDTXMania.Timer);
                    base.b初めての進行描画 = false;
                }
                #region[ 事前に各種数値を定義しておく ]
                double db1ノーツごとの達成率 = (double)this.dbグラフ値目標 / CDTXMania.DTX.n可視チップ数.Drums;
                this.n現在演奏されたノーツ数 =
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Perfect +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Great +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Good +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Poor +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Miss;
                CScoreIni.C演奏記録 drums = new CScoreIni.C演奏記録();
                double rate = (double)n現在演奏されたノーツ数 / (double)CDTXMania.DTX.n可視チップ数.Drums;

                //ゴースト判別
                if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT )
                {
                    bUseGhost = true; //AUTOの場合は無条件で使用可能。
                    this.dbグラフ値目標 = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[ 0 ];
                }
                else if( CDTXMania.ConfigIni.eTargetGhost.Drums != ETargetGhostData.NONE )
                {
                    bUseGhost = true;
                    //ゴーストデータはあるけどゴーストスコアデータが無い場合はScoreIni側から持ってくる。
                    if( CDTXMania.listTargetGhsotLag.Drums != null && CDTXMania.listTargetGhostScoreData.Drums != null )
                    {
                        if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT || CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
                            this.dbグラフ値目標 = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[ 0 ];
                        else
                            this.dbグラフ値目標 = CDTXMania.listTargetGhostScoreData.Drums.db演奏型スキル値;
                    }
                    else
                    {
                        if( CDTXMania.ConfigIni.eTargetGhost.Drums != ETargetGhostData.LAST_PLAY )
                            this.dbグラフ値目標 = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[ 0 ];
                    }
                }
                #endregion

                Rectangle rectangle = new Rectangle( 2, 2, 280, 720 );
                if (this.txグラフ != null)
                {
                    this.txグラフバックパネル.t2D描画( CDTXMania.app.Device, this.n本体X, 50, new Rectangle( 2, 2, 280, 720 ) );
                }

                this.dbグラフ値現在_表示 = this.dbグラフ値現在;
                if( this.txグラフゲージ != null )
                {
                    this.txグラフゲージ.n透明度 = 255;
                    this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 9 + this.n本体X, 650 - ( int )( 560f * this.dbグラフ値現在_表示 / 100.0f ), new Rectangle( 418, 2, 60, (int)( 560f * this.dbグラフ値現在_表示 / 100.0f ) ) );
                }

                //矢印模様
                if( this.txグラフゲージ != null )
                {
                    this.txグラフゲージ.n透明度 = 32;
                    this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 9 + this.n本体X, 90, new Rectangle( 550, 2, 60, 560 ) );

                    this.txグラフゲージ.n透明度 = 255;
                }

                // 基準線
                rectangle = new Rectangle(78, 0, 60, 3);
                for (int i = 0; i < 6; i++)
                {
                    //560.0 * this.dbグラフ値ゴースト_表示 / 100.0
                    //グラフ下部 649
                    int linepos = 0;
                    double target = 0.0;
                    switch( i )
                    {
                        case 0:{ linepos = 0; target = 100.0; }
                            break;
                        case 1:{ linepos = 28; target = 95.0; }
                            break;
                        case 2:{ linepos = 112; target = 80.0; }
                            break;
                        case 3:{ linepos = 151; target = 73.0; }
                            break;
                        case 4:{ linepos = 207; target = 63.0; }
                            break;
                        case 5:{ linepos = 263; target = 53.0; }
                            break;
                    }

                    // 基準線を越えたら線が黄色くなる
                    if( this.dbグラフ値現在 >= target )
                    {
                        rectangle = new Rectangle(420, 556, 60, 2);//黄色
                    }
                    else
                    {
                        rectangle = new Rectangle(420, 566, 60, 2);
                        if (this.txグラフ != null)
                        {
                            this.txグラフ.n透明度 = 160;
                        }
                    }

                    if( this.txグラフ != null && CDTXMania.ConfigIni.nSkillMode == 1 )
                    {
                        this.txグラフ.t2D描画( CDTXMania.app.Device, 8 + this.n本体X, 90 + linepos, rectangle );
                    }
                }

                this.dbグラフ値直前 = this.dbグラフ値現在;

                // --目標値
                this.dbグラフ値目標_表示 = this.dbグラフ値目標;

                db現在の判定数合計 = 0;

                this.dbグラフ値ゴースト_表示 = this.dbグラフ値ゴースト;
                if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.NONE || CDTXMania.listTargetGhsotLag.Drums == null )
                {
                    //ゴーストを使用しない、またはゴーストのラグデータが無い場合は大きさを固定する。
                    this.dbグラフ値ゴースト_表示 = this.dbグラフ値目標_表示;
                }
                rectangle = new Rectangle(138, 0, 72, (int)(556.0 * this.dbグラフ値ゴースト_表示 / 100.0));
                if( this.txグラフゲージ != null )
                {
                    if( this.bUseGhost == false )
                    {
                        this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 75 + this.n本体X, 650 - (int)(560.0 * this.dbグラフ値ゴースト_表示 / 100.0), new Rectangle( 2, 2, 201, (int)( 560.0 * this.dbグラフ値ゴースト_表示 / 100.0 ) ) );
                    }
                    else
                    {
                        if( this.dbグラフ値ゴースト_表示 < this.dbグラフ値現在 )
                        {
                            this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 75 + this.n本体X, 650 - (int)(560.0 * this.dbグラフ値ゴースト_表示 / 100.0), new Rectangle( 2, 2, 201, (int)( 560.0 * this.dbグラフ値ゴースト_表示 / 100.0 ) ) );
                        }
                        else
                        {
                            this.txグラフゲージ.t2D描画( CDTXMania.app.Device, 75 + this.n本体X, 650 - (int)(560.0 * this.dbグラフ値ゴースト_表示 / 100.0), new Rectangle( 210, 2, 201, (int)( 560.0 * this.dbグラフ値ゴースト_表示 / 100.0 ) ) );
                        }
                    }

                }


                this.t小文字表示(204 + this.n本体X, 658, string.Format("{0,6:##0.00}%", this.dbグラフ値現在));
                if( CDTXMania.ConfigIni.nInfoType == 0 ) //達成率表示
                {
                    if( bUseGhost == true )
                    {
                        double dbTargetSkill = 0.0;
                        if( CDTXMania.listTargetGhostScoreData.Drums != null )
                        {
                            dbTargetSkill = CDTXMania.listTargetGhostScoreData.Drums.db演奏型スキル値;
                        }
                        else
                        {
                            dbTargetSkill = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[ 0 ];
                        }

                        switch( CDTXMania.ConfigIni.eTargetGhost.Drums )
                        {
                            case ETargetGhostData.PERFECT:
                            case ETargetGhostData.LAST_PLAY:
                                {
                                    //LASTまたはAUTOの場合は自己ベストも表示させる。
                                    int ntargetPos = 200;
                                    int nbestskillPos = 290;
                                    if( dbTargetSkill <= this.dbグラフ値目標 )
                                    {
                                        ntargetPos = 260;
                                        nbestskillPos = 200;
                                    }


                                    if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
                                    {
                                        if( this.dbグラフ値現在 > dbTargetSkill )
                                            this.tx比較.n透明度 = 128;
                                        this.tx比較.t2D描画( CDTXMania.app.Device, 102 + this.n本体X, ntargetPos, new Rectangle( 288, 2, 162, 85 ) );

                                        this.t小文字表示( 192 + this.n本体X, ntargetPos + 50, string.Format( "{0,6:##0.00}%", dbTargetSkill ) );
                                    }
                                    else
                                    {
                                        if( this.dbグラフ値現在 >= 100.0 )
                                            this.tx比較.n透明度 = 128;
                                        this.tx比較.t2D描画( CDTXMania.app.Device, 102 + this.n本体X, ntargetPos, new Rectangle( 288, 2, 162, 85 ) );

                                        this.t小文字表示( 192 + this.n本体X, ntargetPos + 50, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );
                                    }

                                    if( this.dbグラフ値現在 > this.dbグラフ値目標_表示 )
                                        this.tx比較.n透明度 = 128;
                                    else
                                        this.tx比較.n透明度 = 255;
                                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, nbestskillPos, new Rectangle(288, 160, 162, 60));
                                    this.t小文字表示( 192 + this.n本体X, nbestskillPos + 24, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );


                                    if( this.txPlayerName != null )
                                    {
                                        this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + this.n本体X, ntargetPos + 14 );
                                    }
                                }
                                break;
                            case ETargetGhostData.HI_SCORE:
                            case ETargetGhostData.HI_SKILL:
                                {
                                    this.tx比較.n透明度 = 255;
                                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 200, new Rectangle(288, 160, 162, 60));
                                    this.t小文字表示(186 + this.n本体X, 224, string.Format("{0,6:##0.00}%", dbTargetSkill ));
                                    if( this.dbグラフ値現在 > dbTargetSkill )
                                        this.tx比較.n透明度 = 128;
                                }
                                break;
                        }

                        string strPlus = "";
                        if ( this.dbグラフ値現在 >= this.dbグラフ値ゴースト )
                            strPlus = "+";
                        else
                            strPlus = "-";
                        this.t比較数字表示( 10 + this.n本体X, 668 - (int)(560.0 * this.dbグラフ値現在 / 100.0) - 18, string.Format( strPlus + "{0,5:#0.00}", Math.Abs(this.dbグラフ値現在 - this.dbグラフ値ゴースト) ) );
                        //this.t比較数字表示( 100 + this.n本体X, 668 - (int)(560.0 * this.dbグラフ値ゴースト / 100.0) - 18, string.Format( strPlus + "{0,5:#0.00}", this.dbグラフ値ゴースト ));
                        //CDTXMania.act文字コンソール.tPrint( 75 + this.n本体X[j], 652 - (int)(556.0 * this.dbグラフ値ゴースト_表示 / 100.0) - 18, C文字コンソール.Eフォント種別.白, string.Format( "{0,6:##0.00}%", this.dbグラフ値ゴースト ) );

                    }

                    if( bUseGhost == false )
                    {
                        //比較対象のゴーストデータが無い
                        //ゴーストを使わない
                        this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 200, new Rectangle(288, 160, 162, 60));
                        this.t小文字表示(186 + this.n本体X, 224, string.Format("{0,6:##0.00}%", this.dbグラフ値目標_表示));
                        if (this.dbグラフ値現在 > this.dbグラフ値目標)
                        {
                            this.tx比較.n透明度 = 128;
                        }
                    }
                }
                else if( CDTXMania.ConfigIni.nInfoType == 1 ) //判定数表示
                {
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 200, new Rectangle(288, 226, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 280, new Rectangle(288, 292, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 360, new Rectangle(288, 358, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 440, new Rectangle(288, 424, 162, 60));
                    this.tx比較.t2D描画(CDTXMania.app.Device, 102 + this.n本体X, 520, new Rectangle(288, 490, 162, 60));

                    this.t小文字表示(186 + this.n本体X, 224, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Perfect));
                    this.t小文字表示(186 + this.n本体X, 304, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Great));
                    this.t小文字表示(186 + this.n本体X, 384, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Good));
                    this.t小文字表示(186 + this.n本体X, 464, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Poor));
                    this.t小文字表示(186 + this.n本体X, 544, string.Format("{0,6:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Miss));
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
        private readonly ST文字位置[] st比較数字位置;
        private STキラキラ[] stキラキラ = new STキラキラ[64];
        private STキラキラ[] stフラッシュ = new STキラキラ[16];

        private bool bUseGhost;
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
        private int n本体X;
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
        private void t比較数字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st比較数字位置.Length; i++)
                {
                    if (this.st比較数字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle( 288 + this.st比較数字位置[i].pt.X, 578, 10, 14 );
                        if (this.txグラフバックパネル != null)
                        {
                            this.txグラフバックパネル.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }

                x += 9;

            }
        }
        #endregion
    }
}
