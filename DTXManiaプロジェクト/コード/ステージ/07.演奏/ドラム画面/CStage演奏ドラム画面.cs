using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CStage演奏ドラム画面 : CStage演奏画面共通
	{
		// コンストラクタ

		public CStage演奏ドラム画面()
		{
			base.eステージID = CStage.Eステージ.演奏;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
			base.list子Activities.Add( this.actPad = new CAct演奏Drumsパッド() );
			base.list子Activities.Add( this.actCombo = new CAct演奏DrumsコンボDGB() );
			base.list子Activities.Add( this.actDANGER = new CAct演奏DrumsDanger() );
			base.list子Activities.Add( this.actChipFireD = new CAct演奏DrumsチップファイアD() );
            base.list子Activities.Add( this.actChipFireGB = new CAct演奏DrumsチップファイアGB());
            base.list子Activities.Add( this.actGauge = new CAct演奏Drumsゲージ() );
            base.list子Activities.Add( this.actGraph = new CAct演奏Drumsグラフ() ); // #24074 2011.01.23 add ikanick
			base.list子Activities.Add( this.actJudgeString = new CAct演奏Drums判定文字列() );
			base.list子Activities.Add( this.actLaneFlushD = new CAct演奏DrumsレーンフラッシュD() );
			base.list子Activities.Add( this.actLaneFlushGB = new CAct演奏DrumsレーンフラッシュGB() );
			base.list子Activities.Add( this.actRGB = new CAct演奏DrumsRGB() );
			base.list子Activities.Add( this.actScore = new CAct演奏Drumsスコア() );
			base.list子Activities.Add( this.actStatusPanels = new CAct演奏Drumsステータスパネル() );
			base.list子Activities.Add( this.actWailingBonus = new CAct演奏DrumsWailingBonus() );
			base.list子Activities.Add( this.act譜面スクロール速度 = new CAct演奏スクロール速度() );
			base.list子Activities.Add( this.actAVI = new CAct演奏AVI() );
			base.list子Activities.Add( this.actBGA = new CAct演奏BGA() );
			base.list子Activities.Add( this.actPanel = new CAct演奏パネル文字列() );
			base.list子Activities.Add( this.actStageFailed = new CAct演奏ステージ失敗() );
            //base.list子Activities.Add( this.actStageCleared = new CAct演奏ステージクリア());
			base.list子Activities.Add( this.actPlayInfo = new CAct演奏演奏情報() );
			base.list子Activities.Add( this.actFI = new CActFIFOBlackStart() );
			base.list子Activities.Add( this.actFO = new CActFIFOBlack() );
			base.list子Activities.Add( this.actFOClear = new CActFIFOWhite() );
            base.list子Activities.Add( this.actFOStageClear = new CActFIFOWhiteClear());
            base.list子Activities.Add( this.actFillin = new CAct演奏Drumsフィルインエフェクト() );
		}


		// メソッド

		public void t演奏結果を格納する( out CScoreIni.C演奏記録 Drums, out CScoreIni.C演奏記録 Guitar, out CScoreIni.C演奏記録 Bass, out CDTX.CChip[] r空打ちドラムチップ )
		{
			base.t演奏結果を格納する・ドラム( out Drums );
			base.t演奏結果を格納する・ギター( out Guitar );
			base.t演奏結果を格納する・ベース( out Bass );

			r空打ちドラムチップ = new CDTX.CChip[ 12 ];
			for ( int i = 0; i < 12; i++ )
			{
				r空打ちドラムチップ[ i ] = this.r空うちChip( E楽器パート.DRUMS, (Eパッド) i );
				if( r空打ちドラムチップ[ i ] == null )
				{
					r空打ちドラムチップ[ i ] = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮( CDTXMania.Timer.n現在時刻, this.nパッド0Atoチャンネル0A[ i ], this.nInputAdjustTimeMs.Drums );
				}
			}
		}


		// CStage 実装

		public override void On活性化()
		{
			this.bフィルイン中 = false;
			base.On活性化();
            Cスコア cスコア = CDTXMania.stage選曲.r確定されたスコア;

            this.actChipFireD.iPosY = (CDTXMania.ConfigIni.bReverse.Drums ? -24 : base.nJudgeLinePosY - 186);
            base.actPlayInfo.jl = (CDTXMania.ConfigIni.bReverse.Drums ? 0 : CStage演奏画面共通.nJudgeLineMaxPosY - base.nJudgeLinePosY);

			if( CDTXMania.bコンパクトモード )
			{
				var score = new Cスコア();
				CDTXMania.Songs管理.tScoreIniを読み込んで譜面情報を設定する( CDTXMania.strコンパクトモードファイル + ".score.ini", ref score );
				this.actGraph.dbグラフ値目標_渡 = score.譜面情報.最大スキル[ 0 ];
			}
			else
			{
                this.actGraph.dbグラフ値目標_渡 = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[0];	// #24074 2011.01.23 add ikanick
            }
            dtLastQueueOperation = DateTime.MinValue;
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.bサビ区間 = false;
                this.bボーナス = false;
				this.txチップ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\7_chips.png" ) );
				this.txヒットバー = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\ScreenPlayDrums hit-bar.png" ) );
				this.txヒットバーGB = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\ScreenPlayDrums hit-bar guitar.png" ) );
				this.txレーンフレームGB = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\ScreenPlayDrums lane parts guitar.png" ) );
                this.txシャッター = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_shutter.png" ) );
                this.rResultSound = CDTXMania.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Stage clear.ogg"));
                if( this.txレーンフレームGB != null )
				{
					this.txレーンフレームGB.n透明度 = 0xff - CDTXMania.ConfigIni.n背景の透過度;
				}
                if ((CDTXMania.DTX.bチップがある.LeftCymbal == false) && ((CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false)) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    if (CDTXMania.ConfigIni.eLaneType.Drums == Eタイプ.A)
                    {
                        if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RCRD)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls.png"));
                        }
                        else if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls_RDRC.png"));
                        }
                    }
                    else if (CDTXMania.ConfigIni.eLaneType.Drums == Eタイプ.B)
                    {
                        if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RCRD)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls_TypeB.png"));
                        }
                        else if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls_TypeB_RDRC.png"));
                        }
                    }
                    else if (CDTXMania.ConfigIni.eLaneType.Drums == Eタイプ.C)
                    {
                        if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RCRD)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls.png"));
                        }
                        else if (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC)
                        {
                            this.txLaneCover = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_lanes_Cover_cls_RDRC.png"));
                        }
                    }

                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txヒットバー );
				CDTXMania.tテクスチャの解放( ref this.txヒットバーGB );
				CDTXMania.tテクスチャの解放( ref this.txチップ );
				CDTXMania.tテクスチャの解放( ref this.txレーンフレームGB );
                CDTXMania.tテクスチャの解放( ref this.txLaneCover );
                CDTXMania.tテクスチャの解放( ref this.txシャッター );
                this.rResultSound.Dispose();
                
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			base.sw.Start();
			if( !base.b活性化してない )
            {
                this.bIsFinishedPlaying = false;
                this.bIsFinishedFadeout = false;
                this.bエクセ = false;
                this.bフルコン = false;
                if (base.b初めての進行描画)
                {
                    CSound管理.rc演奏用タイマ.tリセット();
                    CDTXMania.Timer.tリセット();
                    this.actAVI.LivePoint = 0;
                    this.ctチップ模様アニメ.Drums = new CCounter(0, 7, 70, CDTXMania.Timer);
                    int UnitTime;
                    double BPM = CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM;
                    UnitTime = (int)((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 13.0 * 1000.0));
                    this.ctBPMバー = new CCounter(1, 14, UnitTime, CDTXMania.Timer);
                    
                    this.ctチップ模様アニメ.Guitar = new CCounter(0, 0x17, 20, CDTXMania.Timer);
                    this.ctチップ模様アニメ.Bass = new CCounter(0, 0x17, 20, CDTXMania.Timer);
                    this.ctWailingチップ模様アニメ = new CCounter(0, 4, 50, CDTXMania.Timer);
                    base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
                    this.actFI.tフェードイン開始();
                    base.b初めての進行描画 = false;
                }
                if ((CDTXMania.ConfigIni.bSTAGEFAILED有効 && this.actGauge.IsFailed(E楽器パート.DRUMS)) && (base.eフェーズID == CStage.Eフェーズ.共通_通常状態))
                {
                    this.actStageFailed.Start();
                    CDTXMania.DTX.t全チップの再生停止();
                    base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED;
                }
                this.t進行描画・背景();
                this.t進行描画・MIDIBGM();
                this.t進行描画・AVI();
                this.t進行描画・ギターベースフレーム();
                this.t進行描画・レーンフラッシュGB();
                this.t進行描画・レーンフラッシュD();
                this.t進行描画・ギターベース判定ライン();
                this.t進行描画・RGBボタン();
                this.t進行描画・WailingBonus();
                this.t進行描画・譜面スクロール速度();
                this.t進行描画・チップアニメ();
                bIsFinishedPlaying = this.t進行描画・チップ(E楽器パート.DRUMS);
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    if(this.txLaneCover != null)
                        this.txLaneCover.t2D描画(CDTXMania.app.Device, 295, 0);
                }
                int nシャッターIN = (int)(base.nShutterInPosY * 7.2);
                this.txシャッター.t2D描画(CDTXMania.app.Device, 295, (int)(-720 + nシャッターIN));
                int nシャッターOUT = 720 - (int)(base.nShutterOutPosY * 7.2f);
                this.txシャッター.t2D描画(CDTXMania.app.Device, 295, nシャッターOUT);
                this.t進行描画・判定ライン();
                this.t進行描画・ドラムパッド();
                this.t進行描画・スコア();
                this.t進行描画・DANGER();
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
                {
                    this.t進行描画・パネル文字列();
                }
                this.t進行描画・グラフ();   // #24074 2011.01.23 add ikanick
                //this.t進行描画・ステータスパネル();
                //this.t進行描画・コンボ();
                //XG2、グラフON
                if ((CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A ||
                   CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C) &&
                   CDTXMania.ConfigIni.bGraph.Drums == true)
                {
                    if (CDTXMania.ConfigIni.eNamePlate.Drums != Eタイプ.D)
                    {
                        this.t進行描画・ステータスパネル();
                    }
                    this.t進行描画・コンボ();
                }
                //XG2、グラフOFF
                if ((CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A ||
                     CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C) &&
                     CDTXMania.ConfigIni.bGraph.Drums == false)
                {
                    //this.t進行描画・ステータスパネル();
                    if (CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 == Eドラムコンボ文字の表示位置.CENTER)
                        this.t進行描画・コンボ();
                }
                //XG1
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.B)
                {
                    this.t進行描画・ステータスパネル();
                    this.t進行描画・コンボ();
                }
                else if(CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
                {
                    this.t進行描画・コンボ();
                }
                this.t進行描画・ゲージ();
                this.t進行描画・演奏情報();
                this.t進行描画・判定文字列1・通常位置指定の場合();
                this.t進行描画・判定文字列2・判定ライン上指定の場合();
                this.t進行描画・Wailing枠();
                this.t進行描画・チップファイアD();
                this.t進行描画・チップファイアGB();
                this.t進行描画・STAGEFAILED();
                bすべてのチップが判定された = true;
                bIsFinishedFadeout = this.t進行描画・フェードイン・アウト();

                if (bIsFinishedPlaying && (base.eフェーズID == CStage.Eフェーズ.共通_通常状態))
                {

                    this.bサビ区間 = true;
                    UnitTime = 15;
                    ctBPMバー = new CCounter(1, 14, CDTXMania.stage演奏ドラム画面.UnitTime, CDTXMania.Timer);
                    //this.actFOClear.tフェードアウト開始();
                    if (this.actGauge.IsFailed(E楽器パート.DRUMS))
                    {
                        this.actStageFailed.Start();
                        CDTXMania.DTX.t全チップの再生停止();
                        base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED;
                        this.actFO.tフェードアウト開始();
                    }
                    else
                    {
                        this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージクリア;
                        base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト;
                        this.rResultSound.t再生を開始する();
                        this.actFOStageClear.tフェードアウト開始();
//#if dshow
                        this.actFOStageClear.On進行描画( CDTXMania.app.Device );
//#endif
                    }

                }
                if (bIsFinishedFadeout)
                {
                    if (!this.rResultSound.b再生中)
                    {
                        Debug.WriteLine("Total On進行描画=" + sw.ElapsedMilliseconds + "ms");
                        this.nミス数 = base.nヒット数・Auto含まない.Drums.Miss + base.nヒット数・Auto含まない.Drums.Poor;
                        switch (nミス数)
                        {
                            case 0:
                                {
                                    this.nパフェ数 = base.nヒット数・Auto含まない.Drums.Perfect;
                                    if (CDTXMania.ConfigIni.bドラムが全部オートプレイである)
                                    {
                                        this.nパフェ数 = base.nヒット数・Auto含む.Drums.Perfect;
                                    }
                                    if (nパフェ数 == CDTXMania.DTX.n可視チップ数.Drums)
                                    #region[ エクセ ]
                                    {
                                        this.bエクセ = true;
                                        if (CDTXMania.ConfigIni.nSkillMode == 1)
                                            this.actScore.n現在の本当のスコア.Drums += 30000;
                                        break;
                                    }
                                    #endregion
                                    else
                                    #region[ フルコン ]
                                    {
                                        this.bフルコン = true;
                                        if (CDTXMania.ConfigIni.nSkillMode == 1)
                                            this.actScore.n現在の本当のスコア.Drums += 15000;
                                        break;
                                    }
                                    #endregion
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        return (int)this.eフェードアウト完了時の戻り値;
                    }
                }

                // もしサウンドの登録/削除が必要なら、実行する
                if (queueMixerSound.Count > 0)
                {
                    //Debug.WriteLine( "☆queueLength=" + queueMixerSound.Count );
                    DateTime dtnow = DateTime.Now;
                    TimeSpan ts = dtnow - dtLastQueueOperation;
                    if (ts.Milliseconds > 7)
                    {
                        for (int i = 0; i < 2 && queueMixerSound.Count > 0; i++)
                        {
                            dtLastQueueOperation = dtnow;
                            stmixer stm = queueMixerSound.Dequeue();
                            if (stm.bIsAdd)
                            {
                                CDTXMania.Sound管理.AddMixer(stm.csound);
                            }
                            else
                            {
                                CDTXMania.Sound管理.RemoveMixer(stm.csound);
                            }
                        }
                    }
                }
                // キー入力

                if (CDTXMania.act現在入力を占有中のプラグイン == null)
                    this.tキー入力();
            }
			base.sw.Stop();
			return 0;
		}




		// その他

		#region [ private ]
		//-----------------
        public bool bIsFinishedFadeout;
        public bool bIsFinishedPlaying;
        public bool bエクセ;
        public bool bフルコン;
        public bool bすべてのチップが判定された;
        public int nミス数;
        public int nパフェ数;
		private CAct演奏DrumsチップファイアD actChipFireD;
		public CAct演奏Drumsグラフ actGraph;   // #24074 2011.01.23 add ikanick
		public CAct演奏Drumsパッド actPad;
		public bool bフィルイン中;
        public bool bフィルイン終了;
        public bool bサビ区間;
        public bool bボーナス;
        private CSound rResultSound;
		private readonly Eパッド[] eチャンネルtoパッド = new Eパッド[12]
		{
			Eパッド.HH, Eパッド.SD, Eパッド.BD, Eパッド.HT,
			Eパッド.LT, Eパッド.CY, Eパッド.FT, Eパッド.HHO,
			Eパッド.RD, Eパッド.UNKNOWN, Eパッド.UNKNOWN, Eパッド.LC
		};
        private int[] nチャンネルtoX座標 = new int[] { 370, 470, 582, 527, 645, 748, 694, 373, 815, 298, 419, 419 };
        private readonly int[] nチャンネルtoX座標B = new int[] { 370, 419, 533, 596, 645, 748, 694, 373, 815, 298, 476, 476 };
        private readonly int[] nチャンネルtoX座標C = new int[] { 370, 470, 533, 596, 645, 748, 694, 373, 815, 298, 419, 419 };
        private readonly int[] nチャンネルtoX座標D = new int[] { 370, 470, 583, 527, 645, 748, 694, 373, 815, 298, 419, 419 };
        private readonly int[] nチャンネルtoX座標改 = new int[] { 370, 470, 582, 527, 645, 786, 694, 373, 746, 298, 419, 419 };
        private readonly int[] nチャンネルtoX座標B改 = new int[] { 370, 419, 533, 596, 645, 786, 694, 373, 746, 298, 476, 476 };
        private readonly int[] nチャンネルtoX座標C改 = new int[] { 370, 470, 533, 596, 644, 786, 694, 373, 746, 298, 419, 419 };
        private readonly int[] nチャンネルtoX座標D改 = new int[] { 370, 470, 583, 527, 645, 786, 694, 373, 746, 298, 419, 419 };
        //HH SD BD HT LT CY FT HHO RD LC LP LBD
        //レーンタイプB
        //LC 298  HH 371 HHO 374  SD 420  LP 477  BD 534  HT 597 LT 646  FT 695  CY 749  RD 815
        //レーンタイプC

        public int UnitTime;
		private CTexture txヒットバーGB;
		private CTexture txレーンフレームGB;
        public CTexture txシャッター;
        private CTexture txLaneCover;
		//-----------------

        private void tフェードアウト()
        {
            this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージクリア;
            base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト;

            this.actFOStageClear.tフェードアウト開始();
        }

		private bool bフィルイン区間の最後のChipである( CDTX.CChip pChip )
		{
			if( pChip == null )
			{
				return false;
			}
			int num = pChip.n発声位置;
            for (int i = listChip.IndexOf(pChip) + 1; i < listChip.Count; i++)
			{
                pChip = listChip[i];
				if( ( pChip.nチャンネル番号 == 0x53 ) && ( pChip.n整数値 == 2 ) )
				{
					return true;
				}
				if( ( ( pChip.nチャンネル番号 >= 0x11 ) && ( pChip.nチャンネル番号 <= 0x1c ) ) && ( ( pChip.n発声位置 - num ) > 0x18 ) )
				{
					return false;
				}
			}
			return true;
		}

		protected override E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, bool bCorrectLane )
		{
			E判定 eJudgeResult = tチップのヒット処理( nHitTime, pChip, E楽器パート.DRUMS, bCorrectLane );
			// #24074 2011.01.23 add ikanick
            if (CDTXMania.ConfigIni.nSkillMode == 0)
            {
                this.actGraph.dbグラフ値現在_渡 = CScoreIni.t旧演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, E楽器パート.DRUMS, bIsAutoPlay);
            }
            else if (CDTXMania.ConfigIni.nSkillMode == 1)
            {
                this.actGraph.dbグラフ値現在_渡 = CScoreIni.t演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay);
            }
			return eJudgeResult;
		}

		protected override void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part )
		{
			this.tチップのヒット処理・BadならびにTight時のMiss( part, 0, E楽器パート.DRUMS );
		}
		protected override void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part, int nLane )
		{
			this.tチップのヒット処理・BadならびにTight時のMiss( part, nLane, E楽器パート.DRUMS );
		}

        protected override void tJudgeLineMovingUpandDown()
        {
            this.actJudgeString.iP_A = (CDTXMania.ConfigIni.bReverse.Drums ? 159 : this.nJudgeLinePosY - 189);
            this.actJudgeString.iP_B = (CDTXMania.ConfigIni.bReverse.Drums ? 159 : this.nJudgeLinePosY + 23);
            this.actChipFireD.iPosY = (CDTXMania.ConfigIni.bReverse.Drums ? -24 : this.nJudgeLinePosY - 186);
            CDTXMania.stage演奏ドラム画面.actPlayInfo.jl = (CDTXMania.ConfigIni.bReverse.Drums ? 0 : CStage演奏画面共通.nJudgeLineMaxPosY - base.nJudgeLinePosY);
        }

		private bool tドラムヒット処理( long nHitTime, Eパッド type, CDTX.CChip pChip, int n強弱度合い0to127 )
		{
			if( pChip == null )
			{
				return false;
			}
			int index = pChip.nチャンネル番号;
			if ( ( index >= 0x11 ) && ( index <= 0x1c ) )
			{
				index -= 0x11;
			}
			else if ( ( index >= 0x31 ) && ( index <= 60 ) )
			{
				index -= 0x31;
			}
			int nLane = this.nチャンネル0Atoレーン07[ index ];
			int nPad = this.nチャンネル0Atoパッド08[ index ];
			bool bPChipIsAutoPlay = bIsAutoPlay[ nLane ];
			int nInputAdjustTime = bPChipIsAutoPlay ? 0 : this.nInputAdjustTimeMs.Drums;
			E判定 e判定 = this.e指定時刻からChipのJUDGEを返す( nHitTime, pChip, nInputAdjustTime );
			if( e判定 == E判定.Miss )
			{
				return false;
			}
			this.tチップのヒット処理( nHitTime, pChip );
			this.actLaneFlushD.Start( (Eレーン) nLane, ( (float) n強弱度合い0to127 ) / 127f );
			this.actPad.Hit( nPad );
			if( ( e判定 != E判定.Poor ) && ( e判定 != E判定.Miss ) )
			{
				bool flag = this.bフィルイン中;
				bool flag2 = this.bフィルイン中 && this.bフィルイン区間の最後のChipである( pChip );
				this.actChipFireD.Start( (Eレーン) nLane, flag, flag2, flag2 );
			}
			if( CDTXMania.ConfigIni.bドラム打音を発声する )
			{
				CDTX.CChip rChip = null;
				bool bIsChipsoundPriorToPad = true;
				if( ( ( type == Eパッド.HH ) || ( type == Eパッド.HHO ) ) || ( type == Eパッド.LC ) )
				{
					bIsChipsoundPriorToPad = CDTXMania.ConfigIni.eHitSoundPriorityHH == E打ち分け時の再生の優先順位.ChipがPadより優先;
				}
				else if( ( type == Eパッド.LT ) || ( type == Eパッド.FT ) )
				{
					bIsChipsoundPriorToPad = CDTXMania.ConfigIni.eHitSoundPriorityFT == E打ち分け時の再生の優先順位.ChipがPadより優先;
				}
				else if( ( type == Eパッド.CY ) || ( type == Eパッド.RD ) )
				{
					bIsChipsoundPriorToPad = CDTXMania.ConfigIni.eHitSoundPriorityCY == E打ち分け時の再生の優先順位.ChipがPadより優先;
				}
                else if (((type == Eパッド.LP) || (type == Eパッド.LBD)) || (type == Eパッド.BD))
                {
                    bIsChipsoundPriorToPad = CDTXMania.ConfigIni.eHitSoundPriorityLP == E打ち分け時の再生の優先順位.ChipがPadより優先;
                }

				if( bIsChipsoundPriorToPad )
				{
					rChip = pChip;
				}
				else
				{
					Eパッド hH = type;
					if( !CDTXMania.DTX.bチップがある.HHOpen && ( type == Eパッド.HHO ) )
					{
						hH = Eパッド.HH;
					}
					if( !CDTXMania.DTX.bチップがある.Ride && ( type == Eパッド.RD ) )
					{
						hH = Eパッド.CY;
					}
					if( !CDTXMania.DTX.bチップがある.LeftCymbal && ( type == Eパッド.LC ) )
					{
						hH = Eパッド.HH;
					}
					rChip = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮( nHitTime, this.nパッド0Atoチャンネル0A[ (int) hH ], nInputAdjustTime );
					if( rChip == null )
					{
						rChip = pChip;
					}
				}
				this.tサウンド再生( rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums );
			}
			return true;
		}

		protected override void ドラムスクロール速度アップ()
		{
			CDTXMania.ConfigIni.n譜面スクロール速度.Drums = Math.Min( CDTXMania.ConfigIni.n譜面スクロール速度.Drums + 1, 1999 );
		}
		protected override void ドラムスクロール速度ダウン()
		{
			CDTXMania.ConfigIni.n譜面スクロール速度.Drums = Math.Max( CDTXMania.ConfigIni.n譜面スクロール速度.Drums - 1, 0 );
		}

	
		protected override void t進行描画・AVI()
		{
			base.t進行描画・AVI( 0, 0 );
		}
		protected override void t進行描画・BGA()
		{
			base.t進行描画・BGA( 990, 0 );
		}
		protected override void t進行描画・DANGER()
		{
			this.actDANGER.t進行描画( this.actGauge.IsDanger(E楽器パート.DRUMS), false, false );
		}

		protected override void t進行描画・Wailing枠()
		{
			base.t進行描画・Wailing枠( 587, 478,
				CDTXMania.ConfigIni.bReverse.Guitar ? ( 400 - this.txWailing枠.sz画像サイズ.Height ) : 69,
				CDTXMania.ConfigIni.bReverse.Bass ? ( 400 - this.txWailing枠.sz画像サイズ.Height ) : 69
			);
		}

		private void t進行描画・ギターベースフレーム()
		{
			if( ( ( CDTXMania.ConfigIni.eDark != Eダークモード.HALF ) && ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				if( CDTXMania.DTX.bチップがある.Guitar )
				{
					for( int i = 0; i < 355; i += 0x80 )
					{
						Rectangle rectangle = new Rectangle( 0, 0, 0x6d, 0x80 );
						if( ( i + 0x80 ) > 355 )
						{
							rectangle.Height -= ( i + 0x80 ) - 355;
						}
						if( this.txレーンフレームGB != null )
						{
							this.txレーンフレームGB.t2D描画( CDTXMania.app.Device, 0x1fb, 0x39 + i, rectangle );
						}
					}
				}
				if( CDTXMania.DTX.bチップがある.Bass )
				{
					for( int j = 0; j < 355; j += 0x80 )
					{
						Rectangle rectangle2 = new Rectangle( 0, 0, 0x6d, 0x80 );
						if( ( j + 0x80 ) > 355 )
						{
							rectangle2.Height -= ( j + 0x80 ) - 355;
						}
						if( this.txレーンフレームGB != null )
						{
							this.txレーンフレームGB.t2D描画( CDTXMania.app.Device, 0x18e, 0x39 + j, rectangle2 );
						}
					}
				}
			}
		}
		private void t進行描画・ギターベース判定ライン()		// yyagi: ギタレボモードとは座標が違うだけですが、まとめづらかったのでそのまま放置してます。
		{
			if ( ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				if ( CDTXMania.DTX.bチップがある.Guitar )
				{
					int y = ( CDTXMania.ConfigIni.bReverse.Guitar ? 0x176 : 0x5f ) - 3;
					for ( int i = 0; i < 3; i++ )
					{
						if ( this.txヒットバーGB != null )
						{
							this.txヒットバーGB.t2D描画( CDTXMania.app.Device, 0x1fd + ( 0x1a * i ), y );
							this.txヒットバーGB.t2D描画( CDTXMania.app.Device, ( 0x1fd + ( 0x1a * i ) ) + 0x10, y, new Rectangle( 0, 0, 10, 0x10 ) );
						}
					}
				}
				if ( CDTXMania.DTX.bチップがある.Bass )
				{
					int y = ( CDTXMania.ConfigIni.bReverse.Bass ? 0x176 : 0x5f ) - 3;
					for ( int j = 0; j < 3; j++ )
					{
						if ( this.txヒットバーGB != null )
						{
							this.txヒットバーGB.t2D描画( CDTXMania.app.Device, 400 + ( 0x1a * j ), y );
							this.txヒットバーGB.t2D描画( CDTXMania.app.Device, ( 400 + ( 0x1a * j ) ) + 0x10, y, new Rectangle( 0, 0, 10, 0x10 ) );
						}
					}
				}
			}
		}

		private void t進行描画・グラフ()        
        {
            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
            {
            }
            else
            {
                if (CDTXMania.ConfigIni.bGraph.Drums)
                {
                    this.actGraph.On進行描画();
                    this.actGraph.db現在の判定数合計 = this.nヒット数・Auto含む.Drums.Perfect + this.nヒット数・Auto含む.Drums.Great + this.nヒット数・Auto含む.Drums.Good + this.nヒット数・Auto含む.Drums.Miss + this.nヒット数・Auto含む.Drums.Poor;
                }
            }
        }

		private void t進行描画・チップファイアD()
        {
			this.actChipFireD.On進行描画();
        }
        private void t進行描画・ドラムパッド()
        {
            this.actPad.On進行描画();
        }
        protected override void t進行描画・パネル文字列()
        {
            base.t進行描画・パネル文字列(912, 640);
        }

		protected override void t進行描画・演奏情報()
		{
			base.t進行描画・演奏情報( 1000, 257 );
		}

		protected override void t入力処理・ドラム()
        {

            for (int nPad = 0; nPad < (int)Eパッド.MAX; nPad++)
            {
                List<STInputEvent> listInputEvent = CDTXMania.Pad.GetEvents(E楽器パート.DRUMS, (Eパッド)nPad);

                if ((listInputEvent == null) || (listInputEvent.Count == 0))
                    continue;

                this.t入力メソッド記憶(E楽器パート.DRUMS);

                #region [ 打ち分けグループ調整 ]
                //-----------------------------
                EHHGroup eHHGroup = CDTXMania.ConfigIni.eHHGroup;
                EFTGroup eFTGroup = CDTXMania.ConfigIni.eFTGroup;
                ECYGroup eCYGroup = CDTXMania.ConfigIni.eCYGroup;
                EBDGroup eBDGroup = CDTXMania.ConfigIni.eBDGroup;

                if (!CDTXMania.DTX.bチップがある.Ride && (eCYGroup == ECYGroup.打ち分ける))
                {
                    eCYGroup = ECYGroup.共通;
                }
                if (!CDTXMania.DTX.bチップがある.HHOpen && (eHHGroup == EHHGroup.全部打ち分ける))
                {
                    eHHGroup = EHHGroup.左シンバルのみ打ち分ける;
                }
                if (!CDTXMania.DTX.bチップがある.HHOpen && (eHHGroup == EHHGroup.ハイハットのみ打ち分ける))
                {
                    eHHGroup = EHHGroup.全部共通;
                }
                if (!CDTXMania.DTX.bチップがある.LeftCymbal && (eHHGroup == EHHGroup.全部打ち分ける))
                {
                    eHHGroup = EHHGroup.ハイハットのみ打ち分ける;
                }
                if (!CDTXMania.DTX.bチップがある.LeftCymbal && (eHHGroup == EHHGroup.左シンバルのみ打ち分ける))
                {
                    eHHGroup = EHHGroup.全部共通;
                }
                //-----------------------------
                #endregion

                foreach (STInputEvent inputEvent in listInputEvent)
                {
                    /*
                    CDTX.CChip chip28;
                    CDTX.CChip chip29;
                    CDTX.CChip chip30;
                    E判定 e判定25;
                    E判定 e判定26;
                    E判定 e判定27;
                    CDTX.CChip chip32;
                    CDTX.CChip chip33;
                    CDTX.CChip chip34;
                    E判定 e判定28;
                    E判定 e判定29;
                    E判定 e判定30;
                    */

                    if (!inputEvent.b押された)
                        continue;

                    long nTime = inputEvent.nTimeStamp - CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
                    int nInputAdjustTime = this.bIsAutoPlay[base.nチャンネル0Atoレーン07[nPad]] ? 0 : this.nInputAdjustTimeMs.Drums;

                    bool bHitted = false;

                    #region [ (A) ヒットしていればヒット処理して次の inputEvent へ ]
                    //-----------------------------
                    switch (((Eパッド)nPad))
                    {
                        case Eパッド.LC:
                            if (CDTXMania.ConfigIni.bドラム打音を発声する == false && CDTXMania.ConfigIni.bドラムセットを動かす)
                                this.actAVI.ct左シンバル.n現在の値 = 0;
                            break;
                        case Eパッド.CY:
                            if (CDTXMania.ConfigIni.bドラム打音を発声する == false && CDTXMania.ConfigIni.bドラムセットを動かす)
                                this.actAVI.ct右シンバル.n現在の値 = 0;
                            break;
                    }
                    switch (((Eパッド)nPad))
                    {
                        case Eパッド.HH:
                            #region [ HHとLC(groupingしている場合) のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.HH)
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする

                                CDTX.CChip chipHC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x11, nInputAdjustTime);	// HiHat Close
                                CDTX.CChip chipHO = this.r指定時刻に一番近い未ヒットChip(nTime, 0x18, nInputAdjustTime);	// HiHat Open
                                CDTX.CChip chipLC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1a, nInputAdjustTime);	// LC
                                E判定 e判定HC = (chipHC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHC, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定HO = (chipHO != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHO, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LC = (chipLC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLC, nInputAdjustTime) : E判定.Miss;
                                switch (eHHGroup)
                                {
                                    case EHHGroup.ハイハットのみ打ち分ける:
                                        #region [ HCとLCのヒット処理 ]
                                        //-----------------------------
                                        if ((e判定HC != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                    //-----------------------------
                                        #endregion

                                    case EHHGroup.左シンバルのみ打ち分ける:
                                        #region [ HCとHOのヒット処理 ]
                                        //-----------------------------
                                        if ((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                    //-----------------------------
                                        #endregion

                                    case EHHGroup.全部共通:
                                        #region [ HC,HO,LCのヒット処理 ]
                                        //-----------------------------
                                        if (((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss)) && (e判定LC != E判定.Miss))
                                        {
                                            CDTX.CChip chip;
                                            CDTX.CChip[] chipArray = new CDTX.CChip[] { chipHC, chipHO, chipLC };
                                            // ここから、chipArrayをn発生位置の小さい順に並び替える
                                            if (chipArray[1].n発声位置 > chipArray[2].n発声位置)
                                            {
                                                chip = chipArray[1];
                                                chipArray[1] = chipArray[2];
                                                chipArray[2] = chip;
                                            }
                                            if (chipArray[0].n発声位置 > chipArray[1].n発声位置)
                                            {
                                                chip = chipArray[0];
                                                chipArray[0] = chipArray[1];
                                                chipArray[1] = chip;
                                            }
                                            if (chipArray[1].n発声位置 > chipArray[2].n発声位置)
                                            {
                                                chip = chipArray[1];
                                                chipArray[1] = chipArray[2];
                                                chipArray[2] = chip;
                                            }
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipArray[0], inputEvent.nVelocity);
                                            if (chipArray[0].n発声位置 == chipArray[1].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipArray[1], inputEvent.nVelocity);
                                            }
                                            if (chipArray[0].n発声位置 == chipArray[2].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipArray[2], inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HC != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HO != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHO.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            }
                                            else if (chipHO.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipLC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                    //-----------------------------
                                        #endregion

                                    default:
                                        #region [ 全部打ち分け時のヒット処理 ]
                                        //-----------------------------
                                        if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HH, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                    //-----------------------------
                                        #endregion
                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.SD:
                            #region [ SDのヒット処理 ]
                            //-----------------------------
                            if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.SD)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                continue;	// 電子ドラムによる意図的なクロストークを無効にする
                            if (!this.tドラムヒット処理(nTime, Eパッド.SD, this.r指定時刻に一番近い未ヒットChip(nTime, 0x12, nInputAdjustTime), inputEvent.nVelocity))
                                break;
                            continue;
                        //-----------------------------
                            #endregion

                        case Eパッド.BD:
                            #region [ BDとLPとLBD(ペアリングしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                CDTX.CChip chipBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x13, nInputAdjustTime);	// BD
                                CDTX.CChip chipLP = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1b, nInputAdjustTime);	// LP
                                CDTX.CChip chipLBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1c, nInputAdjustTime);	// LBD
                                E判定 e判定BD = (chipBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipBD, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LP = (chipLP != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLP, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LBD = (chipLBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLBD, nInputAdjustTime) : E判定.Miss;
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.BD)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                switch (eBDGroup)
                                {
                                    case EBDGroup.打ち分ける:
                                    case EBDGroup.左右ペダルのみ打ち分ける:
                                        #region[ BDのヒット処理]
                                        if (e判定BD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }
                                        break;
                                        #endregion

                                    case EBDGroup.どっちもBD:
                                        #region[LP&LBD | BD]
                                        if (((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss)) && (e判定BD != E判定.Miss))
                                        {
                                            CDTX.CChip chip8;
                                            CDTX.CChip[] chipArray2 = new CDTX.CChip[] { chipLP, chipLBD, chipBD };
                                            if (chipArray2[1].n発声位置 > chipArray2[2].n発声位置)
                                            {
                                                chip8 = chipArray2[1];
                                                chipArray2[1] = chipArray2[2];
                                                chipArray2[2] = chip8;
                                            }
                                            if (chipArray2[0].n発声位置 > chipArray2[1].n発声位置)
                                            {
                                                chip8 = chipArray2[0];
                                                chipArray2[0] = chipArray2[1];
                                                chipArray2[1] = chip8;
                                            }
                                            if (chipArray2[1].n発声位置 > chipArray2[2].n発声位置)
                                            {
                                                chip8 = chipArray2[1];
                                                chipArray2[1] = chipArray2[2];
                                                chipArray2[2] = chip8;
                                            }
                                            this.tドラムヒット処理(nTime, Eパッド.BD, chipArray2[0], inputEvent.nVelocity);
                                            if (chipArray2[0].n発声位置 == chipArray2[1].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipArray2[1], inputEvent.nVelocity);
                                            }
                                            if (chipArray2[0].n発声位置 == chipArray2[2].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipArray2[2], inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        //chip7 BD  chip6LBD  chip5LP
                                        //判定6 BD  判定5　　 判定4
                                        else if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LP != E判定.Miss) && (e判定BD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        //chip7 BD  chip6LBD  chip5LP
                                        //判定6 BD  判定5　　 判定4
                                        else if ((e判定LBD != E判定.Miss) && (e判定BD != E判定.Miss))
                                        {
                                            if (chipLBD.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLBD, inputEvent.nVelocity);
                                            }
                                            else if (chipLBD.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipLBD, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LP != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.BD, chipLP, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.BD, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定BD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.BD, chipBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }
                                        #endregion
                                        break;

                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.HT:
                            #region [ HTのヒット処理 ]
                            //-----------------------------
                            if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.HT)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                continue;	// 電子ドラムによる意図的なクロストークを無効にする
                            if (this.tドラムヒット処理(nTime, Eパッド.HT, this.r指定時刻に一番近い未ヒットChip(nTime, 20, nInputAdjustTime), inputEvent.nVelocity))
                                continue;
                            break;
                        //-----------------------------
                            #endregion

                        case Eパッド.LT:
                            #region [ LTとFT(groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LT)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipLT = this.r指定時刻に一番近い未ヒットChip(nTime, 0x15, nInputAdjustTime);	// LT
                                CDTX.CChip chipFT = this.r指定時刻に一番近い未ヒットChip(nTime, 0x17, nInputAdjustTime);	// FT
                                E判定 e判定LT = (chipLT != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLT, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定FT = (chipFT != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipFT, nInputAdjustTime) : E判定.Miss;
                                switch (eFTGroup)
                                {
                                    case EFTGroup.打ち分ける:
                                        #region [ LTのヒット処理 ]
                                        //-----------------------------
                                        if (e判定LT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LT, chipLT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        break;
                                    //-----------------------------
                                        #endregion

                                    case EFTGroup.共通:
                                        #region [ LTとFTのヒット処理 ]
                                        //-----------------------------
                                        if ((e判定LT != E判定.Miss) && (e判定FT != E判定.Miss))
                                        {
                                            if (chipLT.n発声位置 < chipFT.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LT, chipLT, inputEvent.nVelocity);
                                            }
                                            else if (chipLT.n発声位置 > chipFT.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LT, chipFT, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LT, chipLT, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LT, chipFT, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LT, chipLT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定FT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LT, chipFT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        break;
                                    //-----------------------------
                                        #endregion
                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.FT:
                            #region [ FTとLT(groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.FT)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipLT = this.r指定時刻に一番近い未ヒットChip(nTime, 0x15, nInputAdjustTime);	// LT
                                CDTX.CChip chipFT = this.r指定時刻に一番近い未ヒットChip(nTime, 0x17, nInputAdjustTime);	// FT
                                E判定 e判定LT = (chipLT != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLT, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定FT = (chipFT != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipFT, nInputAdjustTime) : E判定.Miss;
                                switch (eFTGroup)
                                {
                                    case EFTGroup.打ち分ける:
                                        #region [ FTのヒット処理 ]
                                        //-----------------------------
                                        if (e判定FT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.FT, chipFT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        //-----------------------------
                                        #endregion
                                        break;

                                    case EFTGroup.共通:
                                        #region [ FTとLTのヒット処理 ]
                                        //-----------------------------
                                        if ((e判定LT != E判定.Miss) && (e判定FT != E判定.Miss))
                                        {
                                            if (chipLT.n発声位置 < chipFT.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.FT, chipLT, inputEvent.nVelocity);
                                            }
                                            else if (chipLT.n発声位置 > chipFT.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.FT, chipFT, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.FT, chipLT, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.FT, chipFT, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.FT, chipLT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定FT != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.FT, chipFT, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        //-----------------------------
                                        #endregion
                                        break;
                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.CY:
                            #region [ CY(とLCとRD:groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.CY)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipCY = this.r指定時刻に一番近い未ヒットChip(nTime, 0x16, nInputAdjustTime);	// CY
                                CDTX.CChip chipRD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x19, nInputAdjustTime);	// RD
                                CDTX.CChip chipLC = CDTXMania.ConfigIni.bシンバルフリー ? this.r指定時刻に一番近い未ヒットChip(nTime, 0x1a, nInputAdjustTime) : null;
                                E判定 e判定CY = (chipCY != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipCY, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定RD = (chipRD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipRD, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LC = (chipLC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLC, nInputAdjustTime) : E判定.Miss;
                                CDTX.CChip[] chipArray = new CDTX.CChip[] { chipCY, chipRD, chipLC };
                                E判定[] e判定Array = new E判定[] { e判定CY, e判定RD, e判定LC };
                                const int NumOfChips = 3;	// chipArray.GetLength(0)

                                //num8 = 0;
                                //while( num8 < 2 )

                                // CY/RD/LC群を, n発生位置の小さい順に並べる + nullを大きい方に退かす
                                SortChipsByNTime(chipArray, e判定Array, NumOfChips);
                                //for ( int i = 0; i < NumOfChips - 1; i++ )
                                //{
                                //    //num9 = 2;
                                //    //while( num9 > num8 )
                                //    for ( int j = NumOfChips - 1; j > i; j-- )
                                //    {
                                //        if ( ( chipArray[ j - 1 ] == null ) || ( ( chipArray[ j ] != null ) && ( chipArray[ j - 1 ].n発声位置 > chipArray[ j ].n発声位置 ) ) )
                                //        {
                                //            // swap
                                //            CDTX.CChip chipTemp = chipArray[ j - 1 ];
                                //            chipArray[ j - 1 ] = chipArray[ j ];
                                //            chipArray[ j ] = chipTemp;
                                //            E判定 e判定Temp = e判定Array[ j - 1 ];
                                //            e判定Array[ j - 1 ] = e判定Array[ j ];
                                //            e判定Array[ j ] = e判定Temp;
                                //        }
                                //        //num9--;
                                //    }
                                //    //num8++;
                                //}
                                switch (eCYGroup)
                                {
                                    case ECYGroup.打ち分ける:
                                        #region [打ち分ける]
                                        if (!CDTXMania.ConfigIni.bシンバルフリー)
                                        {
                                            
                                            if (e判定CY != E判定.Miss)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.CY, chipCY, inputEvent.nVelocity);
                                                bHitted = true;
                                            }
                                            if (!bHitted)
                                                break;
                                            continue;
                                        }
                                        //num10 = 0;
                                        //while ( num10 < NumOfChips )
                                        for (int i = 0; i < NumOfChips; i++)
                                        {
                                            if ((e判定Array[i] != E判定.Miss) && ((chipArray[i] == chipCY) || (chipArray[i] == chipLC)))
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.CY, chipArray[i], inputEvent.nVelocity);
                                                bHitted = true;
                                                break;
                                            }
                                            //num10++;
                                        }
                                        if (e判定CY != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.CY, chipCY, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                        #endregion

                                    case ECYGroup.共通:
                                        
                                        if (!CDTXMania.ConfigIni.bシンバルフリー)
                                        {
                                            //num12 = 0;
                                            //while ( num12 < NumOfChips )
                                            for (int i = 0; i < NumOfChips; i++)
                                            {
                                                if ((e判定Array[i] != E判定.Miss) && ((chipArray[i] == chipCY) || (chipArray[i] == chipRD)))
                                                {
                                                    this.tドラムヒット処理(nTime, Eパッド.CY, chipArray[i], inputEvent.nVelocity);
                                                    bHitted = true;
                                                    break;
                                                }
                                                //num12++;
                                            }
                                            if (!bHitted)
                                                break;
                                            continue;
                                        }
                                        //num11 = 0;
                                        //while ( num11 < NumOfChips )
                                        for (int i = 0; i < NumOfChips; i++)
                                        {
                                            if (e判定Array[i] != E判定.Miss)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.CY, chipArray[i], inputEvent.nVelocity);
                                                bHitted = true;
                                                break;
                                            }
                                            //num11++;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.HHO:
                            #region [ HO(とHCとLC:groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.HH)
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする

                                CDTX.CChip chipHC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x11, nInputAdjustTime);	// HC
                                CDTX.CChip chipHO = this.r指定時刻に一番近い未ヒットChip(nTime, 0x18, nInputAdjustTime);	// HO
                                CDTX.CChip chipLC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1a, nInputAdjustTime);	// LC
                                E判定 e判定HC = (chipHC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHC, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定HO = (chipHO != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHO, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LC = (chipLC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLC, nInputAdjustTime) : E判定.Miss;
                                switch (eHHGroup)
                                {
                                    case EHHGroup.全部打ち分ける:
                                        if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;

                                    case EHHGroup.ハイハットのみ打ち分ける:
                                        if ((e判定HO != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHO.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            else if (chipHO.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;

                                    case EHHGroup.左シンバルのみ打ち分ける:
                                        if ((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;

                                    case EHHGroup.全部共通:
                                        if (((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss)) && (e判定LC != E判定.Miss))
                                        {
                                            CDTX.CChip chip;
                                            CDTX.CChip[] chipArray = new CDTX.CChip[] { chipHC, chipHO, chipLC };
                                            // ここから、chipArrayをn発生位置の小さい順に並び替える
                                            if (chipArray[1].n発声位置 > chipArray[2].n発声位置)
                                            {
                                                chip = chipArray[1];
                                                chipArray[1] = chipArray[2];
                                                chipArray[2] = chip;
                                            }
                                            if (chipArray[0].n発声位置 > chipArray[1].n発声位置)
                                            {
                                                chip = chipArray[0];
                                                chipArray[0] = chipArray[1];
                                                chipArray[1] = chip;
                                            }
                                            if (chipArray[1].n発声位置 > chipArray[2].n発声位置)
                                            {
                                                chip = chipArray[1];
                                                chipArray[1] = chipArray[2];
                                                chipArray[2] = chip;
                                            }
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipArray[0], inputEvent.nVelocity);
                                            if (chipArray[0].n発声位置 == chipArray[1].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipArray[1], inputEvent.nVelocity);
                                            }
                                            if (chipArray[0].n発声位置 == chipArray[2].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipArray[2], inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HC != E判定.Miss) && (e判定HO != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipHO.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HC != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHC.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                            }
                                            else if (chipHC.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定HO != E判定.Miss) && (e判定LC != E判定.Miss))
                                        {
                                            if (chipHO.n発声位置 < chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            }
                                            else if (chipHO.n発声位置 > chipLC.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定HC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定HO != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipHO, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LC != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.HHO, chipLC, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                }
                                if (!bHitted)
                                    break;
                                continue;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.RD:
                            #region [ RD(とCYとLC:groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.RD)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipCY = this.r指定時刻に一番近い未ヒットChip(nTime, 0x16, nInputAdjustTime);	// CY
                                CDTX.CChip chipRD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x19, nInputAdjustTime);	// RD
                                CDTX.CChip chipLC = CDTXMania.ConfigIni.bシンバルフリー ? this.r指定時刻に一番近い未ヒットChip(nTime, 0x1a, nInputAdjustTime) : null;
                                E判定 e判定CY = (chipCY != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipCY, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定RD = (chipRD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipRD, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LC = (chipLC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLC, nInputAdjustTime) : E判定.Miss;
                                CDTX.CChip[] chipArray = new CDTX.CChip[] { chipCY, chipRD, chipLC };
                                E判定[] e判定Array = new E判定[] { e判定CY, e判定RD, e判定LC };
                                const int NumOfChips = 3;	// chipArray.GetLength(0)
                                SortChipsByNTime(chipArray, e判定Array, NumOfChips);
                                switch (eCYGroup)
                                {
                                    case ECYGroup.打ち分ける:
                                        if (e判定RD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.RD, chipRD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        break;

                                    case ECYGroup.共通:
                                        if (!CDTXMania.ConfigIni.bシンバルフリー)
                                        {
                                            //num16 = 0;
                                            //while( num16 < 3 )
                                            for (int i = 0; i < NumOfChips; i++)
                                            {
                                                if ((e判定Array[i] != E判定.Miss) && ((chipArray[i] == chipCY) || (chipArray[i] == chipRD)))
                                                {
                                                    this.tドラムヒット処理(nTime, Eパッド.CY, chipArray[i], inputEvent.nVelocity);
                                                    bHitted = true;
                                                    break;
                                                }
                                                //num16++;
                                            }
                                            break;
                                        }
                                        //num15 = 0;
                                        //while( num15 < 3 )
                                        for (int i = 0; i < NumOfChips; i++)
                                        {
                                            if (e判定Array[i] != E判定.Miss)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.CY, chipArray[i], inputEvent.nVelocity);
                                                bHitted = true;
                                                break;
                                            }
                                            //num15++;
                                        }
                                        break;
                                }
                                if (bHitted)
                                {
                                    continue;
                                }
                                break;
                            }
                        //-----------------------------
                            #endregion

                        case Eパッド.LC:
                            #region [ LC(とHC/HOとCYと:groupingしている場合)のヒット処理 ]
                            //-----------------------------
                            {
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LC)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipHC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x11, nInputAdjustTime);	// HC
                                CDTX.CChip chipHO = this.r指定時刻に一番近い未ヒットChip(nTime, 0x18, nInputAdjustTime);	// HO
                                CDTX.CChip chipLC = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1a, nInputAdjustTime);	// LC
                                CDTX.CChip chipCY = CDTXMania.ConfigIni.bシンバルフリー ? this.r指定時刻に一番近い未ヒットChip(nTime, 0x16, nInputAdjustTime) : null;
                                CDTX.CChip chipRD = CDTXMania.ConfigIni.bシンバルフリー ? this.r指定時刻に一番近い未ヒットChip(nTime, 0x19, nInputAdjustTime) : null;
                                E判定 e判定HC = (chipHC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHC, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定HO = (chipHO != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipHO, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LC = (chipLC != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLC, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定CY = (chipCY != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipCY, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定RD = (chipRD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipRD, nInputAdjustTime) : E判定.Miss;
                                CDTX.CChip[] chipArray = new CDTX.CChip[] { chipHC, chipHO, chipLC, chipCY, chipRD };
                                E判定[] e判定Array = new E判定[] { e判定HC, e判定HO, e判定LC, e判定CY, e判定RD };
                                const int NumOfChips = 5;	// chipArray.GetLength(0)
                                SortChipsByNTime(chipArray, e判定Array, NumOfChips);

                                switch (eHHGroup)
                                {
                                    case EHHGroup.全部打ち分ける:
                                    case EHHGroup.左シンバルのみ打ち分ける:
                                        #region[左シンバルのみ打ち分ける]
                                        if (!CDTXMania.ConfigIni.bシンバルフリー)
                                        {
                                            if (e判定LC != E判定.Miss)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LC, chipLC, inputEvent.nVelocity);
                                                bHitted = true;
                                            }
                                            if (!bHitted)
                                                break;
                                            continue;
                                        }
                                        //num5 = 0;
                                        //while( num5 < 5 )
                                        for (int i = 0; i < NumOfChips; i++)
                                        {
                                            if ((e判定Array[i] != E判定.Miss) && (((chipArray[i] == chipLC) || (chipArray[i] == chipCY)) || ((chipArray[i] == chipRD) && (CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通))))
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LC, chipArray[i], inputEvent.nVelocity);
                                                bHitted = true;
                                                break;
                                            }
                                            //num5++;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                        #endregion
                                    case EHHGroup.ハイハットのみ打ち分ける:
                                    case EHHGroup.全部共通:
                                        if (!CDTXMania.ConfigIni.bシンバルフリー)
                                        #region[全部共通]
                                        {
                                            //num7 = 0;
                                            //while( num7 < 5 )
                                            for (int i = 0; i < NumOfChips; i++)
                                            {
                                                if ((e判定Array[i] != E判定.Miss) && (((chipArray[i] == chipLC) || (chipArray[i] == chipHC)) || (chipArray[i] == chipHO)))
                                                {
                                                    this.tドラムヒット処理(nTime, Eパッド.LC, chipArray[i], inputEvent.nVelocity);
                                                    bHitted = true;
                                                    break;
                                                }
                                                //num7++;
                                            }
                                            if (!bHitted)
                                                break;
                                            continue;
                                        }
                                        //num6 = 0;
                                        //while( num6 < 5 )
                                        for (int i = 0; i < NumOfChips; i++)
                                        {
                                            if ((e判定Array[i] != E判定.Miss) && ((chipArray[i] != chipRD) || (CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通)))
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LC, chipArray[i], inputEvent.nVelocity);
                                                bHitted = true;
                                                break;
                                            }
                                            //num6++;
                                        }
                                        if (!bHitted)
                                            break;
                                        continue;
                                        #endregion
                                }
                                if (!bHitted)
                                    break;

                                break;
                            }
                        //-----------------------------
                            #endregion

                        #region [rev030ヒット処理]
                        case Eパッド.LP:
                            #region [ LPのヒット処理 ]
                            //-----------------
                            {
                                //chip28 e判定25 LP
                                //chip29 e判定26 LBD
                                //chip30 e判定27 BD
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LP)
                                    continue;
                                CDTX.CChip chipBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x13, nInputAdjustTime);	// BD
                                CDTX.CChip chipLP = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1b, nInputAdjustTime);	// LP
                                CDTX.CChip chipLBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1c, nInputAdjustTime);	// LBD
                                E判定 e判定BD = (chipBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipBD, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LP = (chipLP != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLP, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LBD = (chipLBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLBD, nInputAdjustTime) : E判定.Miss;
                                switch (eBDGroup)
                                {
                                    case EBDGroup.打ち分ける:
                                    case EBDGroup.左右ペダルのみ打ち分ける:
                                        #region[LP&LBD | BD]
                                        if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定BD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }

                                        break;
                                        #endregion

                                    case EBDGroup.どっちもBD:
                                        #region[LP&LBD&BD]
                                        if (((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss)) && (e判定BD != E判定.Miss))
                                        {
                                            CDTX.CChip chip31;
                                            CDTX.CChip[] chipArray7 = new CDTX.CChip[] { chipLP, chipLBD, chipBD };
                                            if (chipArray7[1].n発声位置 > chipArray7[2].n発声位置)
                                            {
                                                chip31 = chipArray7[1];
                                                chipArray7[1] = chipArray7[2];
                                                chipArray7[2] = chip31;
                                            }
                                            if (chipArray7[0].n発声位置 > chipArray7[1].n発声位置)
                                            {
                                                chip31 = chipArray7[0];
                                                chipArray7[0] = chipArray7[1];
                                                chipArray7[1] = chip31;
                                            }
                                            if (chipArray7[1].n発声位置 > chipArray7[2].n発声位置)
                                            {
                                                chip31 = chipArray7[1];
                                                chipArray7[1] = chipArray7[2];
                                                chipArray7[2] = chip31;
                                            }
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipArray7[0], inputEvent.nVelocity);
                                            if (chipArray7[0].n発声位置 == chipArray7[1].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipArray7[1], inputEvent.nVelocity);
                                            }
                                            if (chipArray7[0].n発声位置 == chipArray7[2].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipArray7[2], inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLBD.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LBD != E判定.Miss) && (e判定BD != E判定.Miss))
                                        {
                                            if (chipLBD.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            }
                                            else if (chipLBD.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LP != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipLP, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定BD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LP, chipBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }
                                        #endregion
                                        break;
                                }
                                if (!this.tドラムヒット処理(nTime, Eパッド.LP, this.r指定時刻に一番近い未ヒットChip(nTime, 0x1b, nInputAdjustTime), inputEvent.nVelocity))
                                    break;
                                continue;
                            }
                        //-----------------
                            #endregion

                        case Eパッド.LBD:
                            #region [ LBDのヒット処理 ]
                            //-----------------
                            {
                                //chip32 e判定28 LP
                                //chip33 e判定29 LBD
                                //chip34 e判定30 BD
                                if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LBD)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                    continue;	// 電子ドラムによる意図的なクロストークを無効にする
                                CDTX.CChip chipBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x13, nInputAdjustTime);	// BD
                                CDTX.CChip chipLP = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1b, nInputAdjustTime);	// LP
                                CDTX.CChip chipLBD = this.r指定時刻に一番近い未ヒットChip(nTime, 0x1c, nInputAdjustTime);	// LBD
                                E判定 e判定BD = (chipBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipBD, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LP = (chipLP != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLP, nInputAdjustTime) : E判定.Miss;
                                E判定 e判定LBD = (chipLBD != null) ? this.e指定時刻からChipのJUDGEを返す(nTime, chipLBD, nInputAdjustTime) : E判定.Miss;
                                switch (eBDGroup)
                                {
                                    case EBDGroup.打ち分ける:
                                        if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }
                                        break;

                                    case EBDGroup.左右ペダルのみ打ち分ける:
                                        #region[左右ペダル打ち分け]
                                        if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LP != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }

                                        break;
                                        #endregion

                                    case EBDGroup.どっちもBD:
                                        if (((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss)) && (e判定BD != E判定.Miss))
                                        {
                                            CDTX.CChip chip35;
                                            CDTX.CChip[] chipArray8 = new CDTX.CChip[] { chipLBD, chipLBD, chipBD };
                                            if (chipArray8[1].n発声位置 > chipArray8[2].n発声位置)
                                            {
                                                chip35 = chipArray8[1];
                                                chipArray8[1] = chipArray8[2];
                                                chipArray8[2] = chip35;
                                            }
                                            if (chipArray8[0].n発声位置 > chipArray8[1].n発声位置)
                                            {
                                                chip35 = chipArray8[0];
                                                chipArray8[0] = chipArray8[1];
                                                chipArray8[1] = chip35;
                                            }
                                            if (chipArray8[1].n発声位置 > chipArray8[2].n発声位置)
                                            {
                                                chip35 = chipArray8[1];
                                                chipArray8[1] = chipArray8[2];
                                                chipArray8[2] = chip35;
                                            }
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipArray8[0], inputEvent.nVelocity);
                                            if (chipArray8[0].n発声位置 == chipArray8[1].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipArray8[1], inputEvent.nVelocity);
                                            }
                                            if (chipArray8[0].n発声位置 == chipArray8[2].n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipArray8[2], inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LP != E判定.Miss) && (e判定LBD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipLBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LP != E判定.Miss) && (e判定BD != E判定.Miss))
                                        {
                                            if (chipLP.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                            }
                                            else if (chipLP.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if ((e判定LBD != E判定.Miss) && (e判定BD != E判定.Miss))
                                        {
                                            if (chipLBD.n発声位置 < chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            }
                                            else if (chipLBD.n発声位置 > chipBD.n発声位置)
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipBD, inputEvent.nVelocity);
                                            }
                                            else
                                            {
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                                this.tドラムヒット処理(nTime, Eパッド.LBD, chipBD, inputEvent.nVelocity);
                                            }
                                            bHitted = true;
                                        }
                                        else if (e判定LP != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipLP, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定LBD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipLBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        else if (e判定BD != E判定.Miss)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LBD, chipBD, inputEvent.nVelocity);
                                            bHitted = true;
                                        }
                                        if (bHitted)
                                        {
                                            continue;
                                        }

                                        break;
                                }


                                if (!this.tドラムヒット処理(nTime, Eパッド.LBD, this.r指定時刻に一番近い未ヒットChip(nTime, 0x1c, nInputAdjustTime), inputEvent.nVelocity))
                                    break;
                                continue;
                            }
                        //-----------------
                            #endregion
#if 封印
                        case Eパッド.LP:
                            #region [ LPのヒット処理 ]
                            //-----------------
                            if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LP)
                                continue;
                            if (!this.tドラムヒット処理(nTime, Eパッド.LP, this.r指定時刻に一番近い未ヒットChip(nTime, 0x1b, nInputAdjustTime), inputEvent.nVelocity))
                                break;
                            continue;
                        //-----------------
                            #endregion

                        case Eパッド.LBD:
                            #region [ LBDのヒット処理 ]
                            //-----------------
                            if (inputEvent.nVelocity <= CDTXMania.ConfigIni.nVelocityMin.LBD)	// #23857 2010.12.12 yyagi: to support VelocityMin
                                continue;	// 電子ドラムによる意図的なクロストークを無効にする
                            if (!this.tドラムヒット処理(nTime, Eパッド.LBD, this.r指定時刻に一番近い未ヒットChip(nTime, 0x1c, nInputAdjustTime), inputEvent.nVelocity))
                                break;
                            continue;
                        //-----------------
                            #endregion
#endif
                        #endregion

                    }
                    //-----------------------------
                    #endregion
                    #region [ (B) ヒットしてなかった場合は、レーンフラッシュ、パッドアニメ、空打ち音再生を実行 ]
                    //-----------------------------
                    this.actLaneFlushD.Start((Eレーン)this.nパッド0Atoレーン07[nPad], ((float)inputEvent.nVelocity) / 127f);
                    this.actPad.Hit(this.nパッド0Atoパッド08[nPad]);
                    switch (((Eパッド)nPad))
                    {
                        case Eパッド.LC:
                            if (CDTXMania.ConfigIni.bドラム打音を発声する == false && CDTXMania.ConfigIni.bドラムセットを動かす)
                                this.actAVI.ct左シンバル.n現在の値 = 0;
                            break;
                        case Eパッド.CY:
                            if (CDTXMania.ConfigIni.bドラム打音を発声する == false && CDTXMania.ConfigIni.bドラムセットを動かす)
                                this.actAVI.ct右シンバル.n現在の値 = 0;
                            break;
                    }

                    if (CDTXMania.ConfigIni.bドラム打音を発声する)
                    {
                        CDTX.CChip rChip = this.r空うちChip(E楽器パート.DRUMS, (Eパッド)nPad);
                        if (rChip != null)
                        {
                            #region [ (B1) 空打ち音が譜面で指定されているのでそれを再生する。]
                            //-----------------
                            this.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                            //-----------------
                            #endregion
                        }
                        else
                        {
                            #region [ (B2) 空打ち音が指定されていないので一番近いチップを探して再生する。]
                            //-----------------
                            switch (((Eパッド)nPad))
                            {
                                case Eパッド.HH:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipHC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[0], nInputAdjustTime);
                                        CDTX.CChip chipHO = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[7], nInputAdjustTime);
                                        CDTX.CChip chipLC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[9], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eHHGroup)
                                        {
                                            case EHHGroup.ハイハットのみ打ち分ける:
                                                rChip = (chipHC != null) ? chipHC : chipLC;
                                                break;

                                            case EHHGroup.左シンバルのみ打ち分ける:
                                                rChip = (chipHC != null) ? chipHC : chipHO;
                                                break;

                                            case EHHGroup.全部共通:
                                                if (chipHC != null)
                                                {
                                                    rChip = chipHC;
                                                }
                                                else if (chipHO == null)
                                                {
                                                    rChip = chipLC;
                                                }
                                                else if (chipLC == null)
                                                {
                                                    rChip = chipHO;
                                                }
                                                else if (chipHO.n発声位置 < chipLC.n発声位置)
                                                {
                                                    rChip = chipHO;
                                                }
                                                else
                                                {
                                                    rChip = chipLC;
                                                }
                                                break;

                                            default:
                                                rChip = chipHC;
                                                break;
                                        }
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.LT:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipLT = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[4], nInputAdjustTime);
                                        CDTX.CChip chipFT = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[5], nInputAdjustTime);
                                        if (CDTXMania.ConfigIni.eFTGroup != EFTGroup.打ち分ける)
                                            rChip = (chipLT != null) ? chipLT : chipFT;
                                        else
                                            rChip = chipLT;
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.FT:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipLT = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[4], nInputAdjustTime);
                                        CDTX.CChip chipFT = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[5], nInputAdjustTime);
                                        if (CDTXMania.ConfigIni.eFTGroup != EFTGroup.打ち分ける)
                                            rChip = (chipFT != null) ? chipFT : chipLT;
                                        else
                                            rChip = chipFT;
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.CY:
                                    #region [ *** ]
                                    //-----------------------------
                                    {

                                        CDTX.CChip chipCY = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[6], nInputAdjustTime);
                                        CDTX.CChip chipRD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[8], nInputAdjustTime);
                                        if (CDTXMania.ConfigIni.eCYGroup != ECYGroup.打ち分ける)
                                            rChip = (chipCY != null) ? chipCY : chipRD;
                                        else
                                            rChip = chipCY;
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.HHO:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipHC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[0], nInputAdjustTime);
                                        CDTX.CChip chipHO = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[7], nInputAdjustTime);
                                        CDTX.CChip chipLC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[9], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eHHGroup)
                                        {
                                            case EHHGroup.全部打ち分ける:
                                                rChip = chipHO;
                                                break;

                                            case EHHGroup.ハイハットのみ打ち分ける:
                                                rChip = (chipHO != null) ? chipHO : chipLC;
                                                break;

                                            case EHHGroup.左シンバルのみ打ち分ける:
                                                rChip = (chipHO != null) ? chipHO : chipHC;
                                                break;

                                            case EHHGroup.全部共通:
                                                if (chipHO != null)
                                                {
                                                    rChip = chipHO;
                                                }
                                                else if (chipHC == null)
                                                {
                                                    rChip = chipLC;
                                                }
                                                else if (chipLC == null)
                                                {
                                                    rChip = chipHC;
                                                }
                                                else if (chipHC.n発声位置 < chipLC.n発声位置)
                                                {
                                                    rChip = chipHC;
                                                }
                                                else
                                                {
                                                    rChip = chipLC;
                                                }
                                                break;
                                        }
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.RD:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipCY = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[6], nInputAdjustTime);
                                        CDTX.CChip chipRD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[8], nInputAdjustTime);
                                        if (CDTXMania.ConfigIni.eCYGroup != ECYGroup.打ち分ける)
                                            rChip = (chipRD != null) ? chipRD : chipCY;
                                        else
                                            rChip = chipRD;
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.LC:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipHC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[0], nInputAdjustTime);
                                        CDTX.CChip chipHO = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[7], nInputAdjustTime);
                                        CDTX.CChip chipLC = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[9], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eHHGroup)
                                        {
                                            case EHHGroup.全部打ち分ける:
                                            case EHHGroup.左シンバルのみ打ち分ける:
                                                rChip = chipLC;
                                                break;

                                            case EHHGroup.ハイハットのみ打ち分ける:
                                            case EHHGroup.全部共通:
                                                if (chipLC != null)
                                                {
                                                    rChip = chipLC;
                                                }
                                                else if (chipHC == null)
                                                {
                                                    rChip = chipHO;
                                                }
                                                else if (chipHO == null)
                                                {
                                                    rChip = chipHC;
                                                }
                                                else if (chipHC.n発声位置 < chipHO.n発声位置)
                                                {
                                                    rChip = chipHC;
                                                }
                                                else
                                                {
                                                    rChip = chipHO;
                                                }
                                                break;
                                        }
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.BD:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[2], nInputAdjustTime);
                                        CDTX.CChip chipLP = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[10], nInputAdjustTime);
                                        CDTX.CChip chipLBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[11], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eBDGroup)
                                        {
                                            case EBDGroup.打ち分ける:
                                                rChip = (chipBD != null) ? chipBD : chipLBD;
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                break;

                                            case EBDGroup.左右ペダルのみ打ち分ける:
                                                rChip = (chipBD != null) ? chipBD : chipLP;
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                break;

                                            case EBDGroup.どっちもBD:
                                                #region [ *** ]
                                                if (chipBD != null)
                                                {
                                                    rChip = chipBD;
                                                }
                                                else if (chipLP == null)
                                                {
                                                    rChip = chipLBD;
                                                }
                                                else if (chipLBD == null)
                                                {
                                                    rChip = chipLP;
                                                }
                                                else if (chipLP.n発声位置 < chipLBD.n発声位置)
                                                {
                                                    rChip = chipLP;
                                                }
                                                else
                                                {
                                                    rChip = chipLBD;
                                                }

                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                #endregion
                                                break;
                                        }
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;


                                case Eパッド.LP:
                                    #region [ *** ]
                                    //-----------------------------
                                    {
                                        CDTX.CChip chipBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[2], nInputAdjustTime);
                                        CDTX.CChip chipLP = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[10], nInputAdjustTime);
                                        CDTX.CChip chipLBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[11], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eBDGroup)
                                        {
                                            case EBDGroup.左右ペダルのみ打ち分ける:
                                                #region[左右ペダル]
                                                rChip = (chipLP != null) ? chipLP : chipBD;
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                if (CDTXMania.ConfigIni.bTight)
                                                {
                                                    this.tチップのヒット処理・BadならびにTight時のMiss(E楽器パート.DRUMS, base.nパッド0Atoレーン07[nPad]);
                                                }

                                                #endregion
                                                break;

                                            case EBDGroup.どっちもBD:
                                                #region[共通]
                                                if (chipLP != null)
                                                {
                                                    rChip = chipLP;
                                                }
                                                else if (chipLBD == null)
                                                {
                                                    rChip = chipBD;
                                                }
                                                else if (chipBD == null)
                                                {
                                                    rChip = chipLBD;
                                                }
                                                else if (chipLBD.n発声位置 < chipBD.n発声位置)
                                                {
                                                    rChip = chipLBD;
                                                }
                                                else
                                                {
                                                    rChip = chipBD;
                                                }
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                if (CDTXMania.ConfigIni.bTight)
                                                {
                                                    this.tチップのヒット処理・BadならびにTight時のMiss(E楽器パート.DRUMS, base.nパッド0Atoレーン07[nPad]);
                                                }
                                                #endregion
                                                break;

                                        }
                                        rChip = chipLP;
                                    }
                                    //-----------------------------
                                    #endregion
                                    break;

                                case Eパッド.LBD:
                                    #region [ *** ]
                                    {
                                        CDTX.CChip chipBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[2], nInputAdjustTime);
                                        CDTX.CChip chipLP = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[10], nInputAdjustTime);
                                        CDTX.CChip chipLBD = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[11], nInputAdjustTime);
                                        switch (CDTXMania.ConfigIni.eBDGroup)
                                        {
                                            case EBDGroup.左右ペダルのみ打ち分ける:
                                                #region [ *** ]
                                                rChip = (chipLBD != null) ? chipLBD : chipBD;
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                if (CDTXMania.ConfigIni.bTight)
                                                {
                                                    this.tチップのヒット処理・BadならびにTight時のMiss(E楽器パート.DRUMS, base.nパッド0Atoレーン07[nPad]);
                                                }
                                                #endregion
                                                break;

                                            case EBDGroup.どっちもBD:
                                                #region[ *** ]
                                                if (chipLBD != null)
                                                {
                                                    rChip = chipLBD;
                                                }
                                                else if (chipLP == null)
                                                {
                                                    rChip = chipBD;
                                                }
                                                else if (chipBD == null)
                                                {
                                                    rChip = chipLP;
                                                }
                                                else if (chipLP.n発声位置 < chipBD.n発声位置)
                                                {
                                                    rChip = chipLP;
                                                }
                                                else
                                                {
                                                    rChip = chipBD;
                                                }
                                                if (rChip != null)
                                                {
                                                    base.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                                                }
                                                if (CDTXMania.ConfigIni.bTight)
                                                {
                                                    this.tチップのヒット処理・BadならびにTight時のMiss(E楽器パート.DRUMS, base.nパッド0Atoレーン07[nPad]);
                                                }
                                                #endregion
                                                break;
                                        }
                                        rChip = chipLBD;
                                    }
                                    #endregion
                                    break;



                                default:
                                    #region [ *** ]
                                    //-----------------------------
                                    rChip = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮(nTime, this.nパッド0Atoチャンネル0A[nPad], nInputAdjustTime);
                                    //-----------------------------
                                    #endregion
                                    break;
                            }
                            if (rChip != null)
                            {
                                // 空打ち音が見つかったので再生する。
                                this.tサウンド再生(rChip, CSound管理.rc演奏用タイマ.nシステム時刻, E楽器パート.DRUMS, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する.Drums);
                            }
                            //-----------------
                            #endregion
                        }
                    }

                    // BAD or TIGHT 時の処理。
                    if (CDTXMania.ConfigIni.bTight)
                        this.tチップのヒット処理・BadならびにTight時のMiss(E楽器パート.DRUMS, this.nパッド0Atoレーン07[nPad]);
                    //-----------------------------
                    #endregion
                }
            }
        }

		// t入力処理・ドラム()からメソッドを抽出したもの。
		/// <summary>
		/// chipArrayの中を, n発生位置の小さい順に並べる + nullを大きい方に退かす。セットでe判定Arrayも並べ直す。
		/// </summary>
		/// <param name="chipArray">ソート対象chip群</param>
		/// <param name="e判定Array">ソート対象e判定群</param>
		/// <param name="NumOfChips">チップ数</param>
		private static void SortChipsByNTime( CDTX.CChip[] chipArray, E判定[] e判定Array, int NumOfChips )
		{
			for ( int i = 0; i < NumOfChips - 1; i++ )
			{
				//num9 = 2;
				//while( num9 > num8 )
				for ( int j = NumOfChips - 1; j > i; j-- )
				{
					if ( ( chipArray[ j - 1 ] == null ) || ( ( chipArray[ j ] != null ) && ( chipArray[ j - 1 ].n発声位置 > chipArray[ j ].n発声位置 ) ) )
					{
						// swap
						CDTX.CChip chipTemp = chipArray[ j - 1 ];
						chipArray[ j - 1 ] = chipArray[ j ];
						chipArray[ j ] = chipTemp;
						E判定 e判定Temp = e判定Array[ j - 1 ];
						e判定Array[ j - 1 ] = e判定Array[ j ];
						e判定Array[ j ] = e判定Temp;
					}
					//num9--;
				}
				//num8++;
			}
		}

		protected override void t背景テクスチャの生成()
		{
            Rectangle bgrect = new Rectangle(980, 0, 0, 0);
            if (CDTXMania.ConfigIni.bBGA有効)
            {
                bgrect = new Rectangle(980, 0, 278, 355);
            }
			string DefaultBgFilename = @"Graphics\7_background.jpg";
			string BgFilename = "";
			if ( ( ( CDTXMania.DTX.BACKGROUND != null ) && ( CDTXMania.DTX.BACKGROUND.Length > 0 ) ) && !CDTXMania.ConfigIni.bストイックモード )
			{
				BgFilename = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.BACKGROUND;
			}
			base.t背景テクスチャの生成( DefaultBgFilename, bgrect, BgFilename );
		}

		protected override void t進行描画・チップ・ドラムス( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( configIni.bDrums有効 )
			{
				#region [ Sudden処理 ]
				if ((configIni.nHidSud == 2) || (configIni.nHidSud == 3))
				{
					if ( pChip.nバーからの距離dot.Drums < 200 )
					{
						pChip.b可視 = true;
						pChip.n透明度 = 0xff;
					}
					else if ( pChip.nバーからの距離dot.Drums < 250 )
					{
						pChip.b可視 = true;
						pChip.n透明度 = 0xff - ( (int) ( ( ( (double) ( pChip.nバーからの距離dot.Drums - 200 ) ) * 255.0 ) / 50.0 ) );
					}
					else
					{
						pChip.b可視 = false;
						pChip.n透明度 = 0;
					}
				}
				#endregion
				#region [ Hidden処理 ]
                if ((configIni.nHidSud == 1) || (configIni.nHidSud == 3))
				{
					if ( pChip.nバーからの距離dot.Drums < 100 )
					{
						pChip.b可視 = false;
					}
					else if ( pChip.nバーからの距離dot.Drums < 150 )
					{
						pChip.b可視 = true;
						pChip.n透明度 = (int) ( ( ( (double) ( pChip.nバーからの距離dot.Drums - 100 ) ) * 255.0 ) / 50.0 );
					}
				}
				#endregion
                #region [ ステルス処理 ]
                if (configIni.nHidSud == 4)
                {
                        pChip.b可視 = false;
                }
                #endregion
				if ( !pChip.bHit && pChip.b可視 )
                {
                    if (this.txチップ != null)
                    {
                        this.txチップ.n透明度 = pChip.n透明度;
                    }
                    int x = this.nチャンネルtoX座標[pChip.nチャンネル番号 - 0x11];

                    if (configIni.eLaneType.Drums == Eタイプ.A)
                    {
                        if (configIni.eRDPosition == ERDPosition.RCRD)
                        {
                            x = this.nチャンネルtoX座標[pChip.nチャンネル番号 - 0x11];
                        }
                        else if(configIni.eRDPosition == ERDPosition.RDRC)
                        {
                            x = this.nチャンネルtoX座標改[pChip.nチャンネル番号 - 0x11];
                        }
                    }
                    else if (configIni.eLaneType.Drums == Eタイプ.B)
                    {
                        if (configIni.eRDPosition == ERDPosition.RCRD)
                        {
                            x = this.nチャンネルtoX座標B[pChip.nチャンネル番号 - 0x11];
                        }
                        else if(configIni.eRDPosition == ERDPosition.RDRC)
                        {
                            x = this.nチャンネルtoX座標B改[pChip.nチャンネル番号 - 0x11];
                        }
                    }
                    else if (configIni.eLaneType.Drums == Eタイプ.C)
                    {
                        if (configIni.eRDPosition == ERDPosition.RCRD)
                        {
                            x = this.nチャンネルtoX座標C[pChip.nチャンネル番号 - 0x11];
                        }
                        else if (configIni.eRDPosition == ERDPosition.RDRC)
                        {
                            x = this.nチャンネルtoX座標C改[pChip.nチャンネル番号 - 0x11];
                        }
                    }
                    else if (configIni.eLaneType.Drums == Eタイプ.D)
                    {
                        if (configIni.eRDPosition == ERDPosition.RCRD)
                        {
                            x = this.nチャンネルtoX座標D[pChip.nチャンネル番号 - 0x11];
                        }
                        else if (configIni.eRDPosition == ERDPosition.RDRC)
                        {
                            x = this.nチャンネルtoX座標D改[pChip.nチャンネル番号 - 0x11];
                        }
                    }

                    if (configIni.eRDPosition == ERDPosition.RDRC)
                    {
                        if (configIni.eLaneType.Drums == Eタイプ.A)
                        {
                            x = this.nチャンネルtoX座標改[pChip.nチャンネル番号 - 0x11];
                        }
                        else if (configIni.eLaneType.Drums == Eタイプ.B)
                        {
                            x = this.nチャンネルtoX座標B改[pChip.nチャンネル番号 - 0x11];
                        }
                    }

                    int y = configIni.bReverse.Drums ? (159 + pChip.nバーからの距離dot.Drums) : (base.nJudgeLinePosY - pChip.nバーからの距離dot.Drums);
                    if (base.txチップ != null)
                    {
                        base.txチップ.vc拡大縮小倍率 = new Vector3((float)pChip.dbチップサイズ倍率, (float)pChip.dbチップサイズ倍率, 1f);
                    }
                    int num9 = this.ctチップ模様アニメ.Drums.n現在の値;

                    switch (pChip.nチャンネル番号)
                    {
                        case 0x11:
                            x = (x + 0x10) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(60, 10 + (num9 * 64), 0x2e, 64));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(60, 0, 0x2e, 10));
                            }
                            break;

                        case 0x12:
                            x = (x + 0x10) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0x6a, 0, 0x36, 10));

                            }
                            break;

                        case 0x13:
                            x = (x + 0x16) - ((int)((44.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(0, 10 + (num9 * 0x40), 60, 0x40));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0, 0, 60, 10));
                            }
                            break;

                        case 0x14:
                            x = (x + 0x10) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(160, 0, 0x2e, 10));
                            }
                            break;

                        case 0x15:
                            x = (x + 0x10) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0xce, 0, 0x2e, 10));
                            }
                            break;

                        case 0x16:
                            x = (x + 19) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(298, 10 + (num9 * 64), 64, 64));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(298, 0, 0x40, 10));
                            }
                            break;

                        case 0x17:
                            x = (x + 0x10) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(0xfc, 10 + (num9 * 64), 0x2e, 0x40));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0xfc, 0, 0x2e, 10));
                            }
                            break;

                        case 0x18:
                            x = (x + 13) - ((int)((26.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                switch (configIni.eHHOGraphics.Drums)
                                {
                                    case Eタイプ.A:
                                        x = (x + 14) - ((int)((26.0 * pChip.dbチップサイズ倍率) / 2.0));
                                        this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 11, new Rectangle(0x200, 10, 0x26, 0x17));
                                        this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0x200, 0, 0x26, 10));
                                        break;

                                    case Eタイプ.B:
                                        x = (x + 14) - ((int)((26.0 * pChip.dbチップサイズ倍率) / 2.0));
                                        this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0x200, 0, 0x26, 10));
                                        break;

                                    case Eタイプ.C:                                        
                                        x = (x + 13) - ((int)((32.0 * pChip.dbチップサイズ倍率) / 2.0));
                                        this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(60, 10 + (num9 * 64), 0x2e, 64));
                                        this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(60, 0, 0x2e, 10));
                                        break;
                                }
                            }
                                break;

                        case 0x19:
                            x = (x + 13) - ((int)((26.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 0x20, new Rectangle(0x16a, 10 + (num9 * 64), 0x26, 0x40));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0x16a, 0, 0x26, 10));
                            }
                            break;

                        case 0x1a:
                            x = (x + 0x13) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(448, 10 + (num9 * 64), 64, 64));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(448, 0, 64, 10));

                            }
                            break;

                        case 0x1b:
                            x = (x + 0x13) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 0x20, new Rectangle(550, 10 + (num9 * 64), 0x30, 0x40));
                                this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0, 522, 0x30, 10));
                            }
                            break;
                        case 0x1c:
                            x = (x + 0x13) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                            if (this.txチップ != null)
                            {

                                if (configIni.eLBDGraphics.Drums == Eタイプ.A)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 0x20, new Rectangle(550, 10 + (num9 * 64), 0x30, 0x40));
                                    this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(0, 522, 0x30, 10));

                                }
                                else if (configIni.eLBDGraphics.Drums == Eタイプ.B)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 0x20, new Rectangle(400, 10 + (num9 * 64), 0x30, 0x40));
                                    this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(400, 0, 0x30, 10));
                                }
                            }
                            break;
                            /*
                        case 0x4F:
                            if (this.txチップ != null)
                            {
                                switch (pChip.n整数値)
                                {
                                    case 0x01:
                                        x = (x + 0x13) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                                        if (this.txチップ != null)
                                        {
                                            this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(448, 10 + (num9 * 64), 64, 64));
                                            this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(448, 0, 64, 10));

                                        }
                                        break;
                                    case 0x09:
                                        x = (x + 19) - ((int)((38.0 * pChip.dbチップサイズ倍率) / 2.0));
                                        if (this.txチップ != null)
                                        {
                                            this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 32, new Rectangle(298, 10 + (num9 * 64), 64, 64));
                                            this.txチップ.t2D描画(CDTXMania.app.Device, x, y - 5, new Rectangle(298, 0, 0x40, 10));
                                        }
                                        break;
                                }
                            }
                            break;
                            */
                    }
                    if (this.txチップ != null)
                    {
                        this.txチップ.vc拡大縮小倍率 = new Vector3(1f, 1f, 1f);
                        this.txチップ.n透明度 = 0xff;
                    }
                }

				int indexSevenLanes = this.nチャンネル0Atoレーン07[ pChip.nチャンネル番号 - 0x11 ];
				if ( ( configIni.bAutoPlay[ indexSevenLanes ] && !pChip.bHit ) && ( pChip.nバーからの距離dot.Drums < 0 ) )
				{
					pChip.bHit = true;
					this.actLaneFlushD.Start( (Eレーン) indexSevenLanes, ( (float) CInput管理.n通常音量 ) / 127f );
					bool flag = this.bフィルイン中;
					bool flag2 = this.bフィルイン中 && this.bフィルイン区間の最後のChipである( pChip );
					//bool flag3 = flag2;
					this.actChipFireD.Start( (Eレーン) indexSevenLanes, flag, flag2, flag2 );
					this.actPad.Hit( this.nチャンネル0Atoパッド08[ pChip.nチャンネル番号 - 0x11 ] );
					this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, E楽器パート.DRUMS, dTX.nモニタを考慮した音量( E楽器パート.DRUMS ) );
					this.tチップのヒット処理( pChip.n発声時刻ms, pChip );
				}
				return;
			}	// end of "if configIni.bDrums有効"
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
                this.tサウンド再生(pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, E楽器パート.DRUMS, dTX.nモニタを考慮した音量(E楽器パート.DRUMS));
				pChip.bHit = true;
			}
		}
		protected override void t進行描画・チップ・ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst )
		{
			base.t進行描画・チップ・ギターベース( configIni, ref dTX, ref pChip, inst,
				95, 374, 57, 412, 509, 400,
				268, 144, 76, 6,
				24, 509, 561, 400, 452, 26, 24 );
		}
		protected override void t進行描画・チップ・ギター・ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( configIni.bGuitar有効 )
			{
				//if ( configIni.bSudden.Guitar )
				//{
				//    pChip.b可視 = pChip.nバーからの距離dot.Guitar < 200;
				//}
				//if ( configIni.bHidden.Guitar && ( pChip.nバーからの距離dot.Guitar < 100 ) )
				//{
				//    pChip.b可視 = false;
				//}

				// 後日、以下の部分を何とかCStage演奏画面共通.csに移したい。
				if ( !pChip.bHit && pChip.b可視 )
				{
					int[] y_base = { 0x5f, 0x176 };		// 判定バーのY座標: ドラム画面かギター画面かで変わる値
					int offset = 0x39;					// ドラム画面かギター画面かで変わる値

					const int WailingWidth = 20;		// ウェイリングチップ画像の幅: 4種全て同じ値
					const int WailingHeight = 50;		// ウェイリングチップ画像の高さ: 4種全て同じ値
					const int baseTextureOffsetX = 268;	// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
					const int baseTextureOffsetY = 174;	// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値
					const int drawX = 588;				// ウェイリングチップ描画位置X座標: 4種全て異なる値

					const int numA = 25;				// 4種全て同じ値
					int y = configIni.bReverse.Guitar ? ( y_base[1] - pChip.nバーからの距離dot.Guitar ) : ( y_base[0] + pChip.nバーからの距離dot.Guitar );
					int numB = y - offset;				// 4種全て同じ定義
					int numC = 0;						// 4種全て同じ初期値
					const int numD = 355;				// ドラム画面かギター画面かで変わる値
					if ( ( numB < ( numD + numA ) ) && ( numB > -numA ) )	// 以下のロジックは4種全て同じ
					{
						int c = this.ctWailingチップ模様アニメ.n現在の値;
						Rectangle rect = new Rectangle( baseTextureOffsetX + ( c * WailingWidth ), baseTextureOffsetY, WailingWidth, WailingHeight);
						if ( numB < numA )
						{
							rect.Y += numA - numB;
							rect.Height -= numA - numB;
							numC = numA - numB;
						}
						if ( numB > ( numD - numA ) )
						{
							rect.Height -= numB - ( numD - numA );
						}
						if ( ( rect.Bottom > rect.Top ) && ( this.txチップ != null ) )
						{
							this.txチップ.t2D描画( CDTXMania.app.Device, drawX, ( y - numA ) + numC, rect );
						}
					}
				}
				//    if ( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
				//    {
				//        if ( pChip.nバーからの距離dot.Guitar < -234 )	// #25253 2011.5.29 yyagi: Don't set pChip.bHit=true for wailing at once. It need to 1sec-delay (234pix per 1sec). 
				//        {
				//            pChip.bHit = true;
				//        }
				//        if ( configIni.bAutoPlay.Guitar )
				//        {
				//            pChip.bHit = true;						// #25253 2011.5.29 yyagi: Set pChip.bHit=true if autoplay.
				//            this.actWailingBonus.Start( E楽器パート.GUITAR, this.r現在の歓声Chip.Guitar );
				//        }
				//    }
				//    return;
				//}
				//pChip.bHit = true;
			}
			base.t進行描画・チップ・ギター・ウェイリング( configIni, ref dTX, ref pChip );
		}
		protected override void t進行描画・チップ・フィルイン( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
				pChip.bHit = true;
				switch ( pChip.n整数値 )
				{
					case 0x01:	// フィルイン開始
                        this.bフィルイン終了 = true;
						if ( configIni.bフィルイン有効 )
						{
							this.bフィルイン中 = true;
						}
						break;

					case 0x02:	// フィルイン終了
                        this.bフィルイン終了 = true;
						if ( configIni.bフィルイン有効 )
						{
							this.bフィルイン中 = false;
						}
                        if (((this.actCombo.n現在のコンボ数.Drums > 0) || configIni.bドラムが全部オートプレイである) && configIni.b歓声を発声する)
                        {
                            this.actAVI.Start(bフィルイン中);
                            if (this.r現在の歓声Chip.Drums != null)
                            {
                                dTX.tチップの再生(this.r現在の歓声Chip.Drums, CSound管理.rc演奏用タイマ.nシステム時刻, (int)Eレーン.BGM, dTX.nモニタを考慮した音量(E楽器パート.UNKNOWN));
                            }
                            else
                            {
                                CDTXMania.Skin.sound歓声音.t再生する();
                                CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            }
                            //if (CDTXMania.ConfigIni.nSkillMode == 1)
                            //    this.actScore.n現在の本当のスコア.Drums += 500;
                        }
						break;
                    case 0x03:
                        this.bサビ区間 = true;
                        break;
                    case 0x04:
                        this.bサビ区間 = false;
                        break;
                    case 0x05:
                        if (configIni.bフィルイン有効)
                        {
                            this.bサビ区間 = true;
                        }
                        if (((this.actCombo.n現在のコンボ数.Drums > 0) || configIni.bドラムが全部オートプレイである) && configIni.b歓声を発声する && configIni.ボーナス演出を表示する)
                        {
                            this.actAVI.Start(true);
                            if (this.r現在の歓声Chip.Drums != null)
                            {
                                dTX.tチップの再生(this.r現在の歓声Chip.Drums, CSound管理.rc演奏用タイマ.nシステム時刻, (int)Eレーン.BGM, dTX.nモニタを考慮した音量(E楽器パート.UNKNOWN));
                            }
                            else
                            {
                                CDTXMania.Skin.sound歓声音.t再生する();
                                CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            }
                            //if (CDTXMania.ConfigIni.nSkillMode == 1)
                            //this.actScore.n現在の本当のスコア.Drums += 500;
                        }
                        break;
                    case 0x06:
                        if (configIni.bフィルイン有効)
                        {
                            this.bサビ区間 = false;
                        }
                        if (((this.actCombo.n現在のコンボ数.Drums > 0) || configIni.bドラムが全部オートプレイである) && configIni.b歓声を発声する && configIni.ボーナス演出を表示する)
                        {
                            this.actAVI.Start(true);
                            if (this.r現在の歓声Chip.Drums != null)
                            {
                                dTX.tチップの再生(this.r現在の歓声Chip.Drums, CSound管理.rc演奏用タイマ.nシステム時刻, (int)Eレーン.BGM, dTX.nモニタを考慮した音量(E楽器パート.UNKNOWN));
                            }
                            else
                            {
                                CDTXMania.Skin.sound歓声音.t再生する();
                                CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            }
                            //if(CDTXMania.ConfigIni.nSkillMode == 1)
                            //this.actScore.n現在の本当のスコア.Drums += 500;
                        }
                        break;
				}
			}
		}

        
        protected override void t進行描画・チップ・ボーナス(CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip)
        {
            if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
            {
                pChip.bHit = true;
                bボーナス = true;
                if (((this.actCombo.n現在のコンボ数.Drums > 0) || configIni.bドラムが全部オートプレイである) && configIni.b歓声を発声する)
                {
                    switch (pChip.n整数値)
                    {
                        case 0x01: //LC
                            this.actPad.Start(0, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x02: //HH
                            this.actPad.Start(1, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x03: //LP
                            this.actPad.Start(2, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x04: //SD
                            this.actPad.Start(3, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x05: //HT
                            this.actPad.Start(4, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x06: //BD
                            this.actPad.Start(5, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x07: //LT
                            this.actPad.Start(6, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x08: //FT
                            this.actPad.Start(7, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x09: //CY
                            this.actPad.Start(8, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        case 0x0A: //RD
                            this.actPad.Start(9, true);
                            CDTXMania.Skin.sound歓声音.t再生する();
                            CDTXMania.Skin.sound歓声音.n位置・次に鳴るサウンド = 0;
                            break;

                        default:
                            break;
                    }
                    if(configIni.ボーナス演出を表示する)
                        this.actAVI.Start(true);
                }
            }

        }
        

		protected override void t進行描画・チップ・ベース・ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( configIni.bGuitar有効 )
			{
				//if ( configIni.bSudden.Bass )
				//{
				//    pChip.b可視 = pChip.nバーからの距離dot.Bass < 200;
				//}
				//if ( configIni.bHidden.Bass && ( pChip.nバーからの距離dot.Bass < 100 ) )
				//{
				//    pChip.b可視 = false;
				//}

				//
				// 後日、以下の部分を何とかCStage演奏画面共通.csに移したい。
				//
				if ( !pChip.bHit && pChip.b可視 )
				{
					int[] y_base = { 0x5f, 0x176 };		// 判定バーのY座標: ドラム画面かギター画面かで変わる値
					int offset = 0x39;					// ドラム画面かギター画面かで変わる値

					const int WailingWidth = 20;		// ウェイリングチップ画像の幅: 4種全て同じ値
					const int WailingHeight = 50;		// ウェイリングチップ画像の高さ: 4種全て同じ値
					const int baseTextureOffsetX = 268;	// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
					const int baseTextureOffsetY = 174;	// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値
					const int drawX = 479;				// ウェイリングチップ描画位置X座標: 4種全て異なる値

					const int numA = 25;				// 4種全て同じ値
					int y = configIni.bReverse.Bass ? ( y_base[ 1 ] - pChip.nバーからの距離dot.Bass ) : ( y_base[ 0 ] + pChip.nバーからの距離dot.Bass );
					int numB = y - offset;				// 4種全て同じ定義
					int numC = 0;						// 4種全て同じ初期値
					const int numD = 355;				// ドラム画面かギター画面かで変わる値
					if ( ( numB < ( numD + numA ) ) && ( numB > -numA ) )	// 以下のロジックは4種全て同じ
					{
						int c = this.ctWailingチップ模様アニメ.n現在の値;
						Rectangle rect = new Rectangle( baseTextureOffsetX + ( c * WailingWidth ), baseTextureOffsetY, WailingWidth, WailingHeight );
						if ( numB < numA )
						{
							rect.Y += numA - numB;
							rect.Height -= numA - numB;
							numC = numA - numB;
						}
						if ( numB > ( numD - numA ) )
						{
							rect.Height -= numB - ( numD - numA );
						}
						if ( ( rect.Bottom > rect.Top ) && ( this.txチップ != null ) )
						{
							this.txチップ.t2D描画( CDTXMania.app.Device, drawX, ( y - numA ) + numC, rect );
						}
					}
				}
				//    if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
				//    {
				//        if ( pChip.nバーからの距離dot.Bass < -234 )	// #25253 2011.5.29 yyagi: Don't set pChip.bHit=true for wailing at once. It need to 1sec-delay (234pix per 1sec).
				//        {
				//            pChip.bHit = true;
				//        }
				//        if ( configIni.bAutoPlay.Bass )
				//        {
				//            this.actWailingBonus.Start( E楽器パート.BASS, this.r現在の歓声Chip.Bass );
				//            pChip.bHit = true;						// #25253 2011.5.29 yyagi: Set pChip.bHit=true if autoplay.
				//        }
				//    }
				//    return;
				//}
				//pChip.bHit = true;
			}
				base.t進行描画・チップ・ベース・ウェイリング( configIni, ref dTX, ref pChip);
		}
        protected override void t進行描画・チップ・空打ち音設定・ドラム(CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip)
        {
            if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
            {
                pChip.bHit = true;
                this.r現在の空うちドラムChip[(int)this.eチャンネルtoパッド[pChip.nチャンネル番号 - 0xb1]] = pChip;
                pChip.nチャンネル番号 = ((pChip.nチャンネル番号 < 0xbc) || (pChip.nチャンネル番号 > 190)) ? ((pChip.nチャンネル番号 - 0xb1) + 0x11) : ((pChip.nチャンネル番号 - 0xb3) + 0x11);
            }
        }
		protected override void t進行描画・チップ・小節線( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			int n小節番号plus1 = pChip.n発声位置 / 384;
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
				pChip.bHit = true;
				this.actPlayInfo.n小節番号 = n小節番号plus1 - 1;
                if ( configIni.bWave再生位置自動調整機能有効 && bIsDirectSound )
				{
					dTX.tWave再生位置自動補正();
				}
			}
			if ( configIni.bDrums有効 )
			{
				if ( configIni.b演奏情報を表示する && ( configIni.eDark == Eダークモード.OFF ) )
                {
                    if (CDTXMania.ConfigIni.nInfoType == 0)
                    {
                        int n小節番号 = n小節番号plus1 - 1;
                        CDTXMania.act文字コンソール.tPrint(858, configIni.bReverse.Drums ? ((159 + pChip.nバーからの距離dot.Drums) - 0x11) : ((base.nJudgeLinePosY - pChip.nバーからの距離dot.Drums) - 0x11), C文字コンソール.Eフォント種別.白, n小節番号.ToString());
                    }
				}
				if ( ( ( configIni.eDark != Eダークモード.FULL ) && pChip.b可視 ) && ( this.txチップ != null ) )
				{
                    this.txチップ.t2D描画(CDTXMania.app.Device, 295, configIni.bReverse.Drums ? ((159 + pChip.nバーからの距離dot.Drums) - 1) : ((base.nJudgeLinePosY - pChip.nバーからの距離dot.Drums) - 1), new Rectangle(0, 769, 0x22f, 2));
				}
                
			}
			if ( ( pChip.b可視 && configIni.bGuitar有効 ) && ( configIni.eDark != Eダークモード.FULL ) )
			{
				int y = configIni.bReverse.Guitar ? ( ( 0x176 - pChip.nバーからの距離dot.Guitar ) - 1 ) : ( ( 0x5f + pChip.nバーからの距離dot.Guitar ) - 1 );
				if ( ( dTX.bチップがある.Guitar && ( y > 0x39 ) ) && ( ( y < 0x19c ) && ( this.txチップ != null ) ) )
				{
					this.txチップ.t2D描画( CDTXMania.app.Device, 374, y, new Rectangle( 0, 450, 0x4e, 1 ) );
				}
				y = configIni.bReverse.Bass ? ( ( 0x176 - pChip.nバーからの距離dot.Bass ) - 1 ) : ( ( 0x5f + pChip.nバーからの距離dot.Bass ) - 1 );
				if ( ( dTX.bチップがある.Bass && ( y > 0x39 ) ) && ( ( y < 0x19c ) && ( this.txチップ != null ) ) )
				{
					this.txチップ.t2D描画( CDTXMania.app.Device, 398, y, new Rectangle( 0, 450, 0x4e, 1 ) );
				}
			}
		}              //移植完了。
    #endregion
	}

}
