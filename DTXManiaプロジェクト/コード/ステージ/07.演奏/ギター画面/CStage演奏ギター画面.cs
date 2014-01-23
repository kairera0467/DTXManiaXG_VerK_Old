﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CStage演奏ギター画面 : CStage演奏画面共通
	{
		// コンストラクタ

		public CStage演奏ギター画面()
		{
			base.eステージID = CStage.Eステージ.演奏;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
            base.list子Activities.Add( this.actBPMBar = new CAct演奏GuitarBPMバー() );
			base.list子Activities.Add( this.actStageFailed = new CAct演奏ステージ失敗() );
			base.list子Activities.Add( this.actDANGER = new CAct演奏GuitarDanger() );
			base.list子Activities.Add( this.actAVI = new CAct演奏AVI() );
			base.list子Activities.Add( this.actBGA = new CAct演奏BGA() );
            base.list子Activities.Add( this.actGraph = new CAct演奏Guitarグラフ() );
			base.list子Activities.Add( this.actPanel = new CAct演奏パネル文字列() );
			base.list子Activities.Add( this.act譜面スクロール速度 = new CAct演奏スクロール速度() );
			base.list子Activities.Add( this.actStatusPanels = new CAct演奏Guitarステータスパネル() );
			base.list子Activities.Add( this.actWailingBonus = new CAct演奏GuitarWailingBonus() );
            base.list子Activities.Add( this.actLivePoint = new CAct演奏GuitarLivePoint() );
			base.list子Activities.Add( this.actScore = new CAct演奏Guitarスコア() );
			base.list子Activities.Add( this.actRGB = new CAct演奏GuitarRGB() );
			base.list子Activities.Add( this.actLaneFlushGB = new CAct演奏GuitarレーンフラッシュGB() );
			base.list子Activities.Add( this.actJudgeString = new CAct演奏Guitar判定文字列() );
			base.list子Activities.Add( this.actGauge = new CAct演奏Guitarゲージ() );
			base.list子Activities.Add( this.actCombo = new CAct演奏Guitarコンボ() );
			base.list子Activities.Add( this.actChipFireGB = new CAct演奏Guitarチップファイア() );
			base.list子Activities.Add( this.actPlayInfo = new CAct演奏演奏情報() );
			base.list子Activities.Add( this.actFI = new CActFIFOBlackStart() );
			base.list子Activities.Add( this.actFO = new CActFIFOBlack() );
			base.list子Activities.Add( this.actFOClear = new CActFIFOWhite() );
            base.list子Activities.Add( this.actFOStageClear = new CActFIFOWhiteClear());
		}


		// メソッド

		public void t演奏結果を格納する( out CScoreIni.C演奏記録 Drums, out CScoreIni.C演奏記録 Guitar, out CScoreIni.C演奏記録 Bass )
		{
			Drums = new CScoreIni.C演奏記録();

			base.t演奏結果を格納する・ギター( out Guitar );
			base.t演奏結果を格納する・ベース( out Bass );

//			if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )		// #24063 2011.1.24 yyagi Gt/Bsを入れ替えていたなら、演奏結果も入れ替える
//			{
//				CScoreIni.C演奏記録 t;
//				t = Guitar;
//				Guitar = Bass;
//				Bass = t;
//			
//				CDTXMania.DTX.SwapGuitarBassInfos();			// 譜面情報も元に戻す
//			}
		}
		

		// CStage 実装

		public override void On活性化()
		{
            this.ct登場用 = new CCounter(0, 12, 16, CDTXMania.Timer);
            dtLastQueueOperation = DateTime.MinValue;
            if (CDTXMania.bコンパクトモード)
            {
                var score = new Cスコア();
                CDTXMania.Songs管理.tScoreIniを読み込んで譜面情報を設定する(CDTXMania.strコンパクトモードファイル + ".score.ini", ref score);
                this.actGraph.dbグラフ値目標_渡G = score.譜面情報.最大スキル[1];
            }
            else
            {
                this.actGraph.dbグラフ値目標_渡G = CDTXMania.stage選曲.r確定されたスコア.譜面情報.最大スキル[1];	// #24074 2011.01.23 add ikanick
            }
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.bサビ区間 = false;
				//this.t背景テクスチャの生成();
				this.txチップ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Chips_Guitar.png" ) );
                this.txレーン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_lanes_Guitar.png") );
                this.txヒットバー = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\ScreenPlayDrums hit-bar.png"));
				//this.txWailing枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay wailing cursor.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				//CDTXMania.tテクスチャの解放( ref this.tx背景 );
				CDTXMania.tテクスチャの解放( ref this.txチップ );
                CDTXMania.tテクスチャの解放( ref this.txレーン );
				CDTXMania.tテクスチャの解放( ref this.txヒットバー );
				//CDTXMania.tテクスチャの解放( ref this.txWailing枠 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                bool flag = false;
                bool flag2 = false;

				if( base.b初めての進行描画 )
				{
                    CSound管理.rc演奏用タイマ.tリセット();
					CDTXMania.Timer.tリセット();

                    this.UnitTime = ((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 14.0)); //2014.01.14.kairera0467 これも動かしたいのだが・・・・
                    this.actBPMBar.ctBPMバー = new CCounter(1.0, 14.0, UnitTime, CSound管理.rc演奏用タイマ);

					this.ctチップ模様アニメ.Guitar = new CCounter( 0, 0x17, 20, CDTXMania.Timer );
					this.ctチップ模様アニメ.Bass = new CCounter( 0, 0x17, 20, CDTXMania.Timer );
					this.ctチップ模様アニメ[ 0 ] = null;
                    this.ctコンボ動作タイマ = new CCounter(1, 16, (int)((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0 * 1000.0)), CDTXMania.Timer);
					this.ctWailingチップ模様アニメ = new CCounter( 0, 4, 50, CDTXMania.Timer );

                    this.actLivePoint.n現在のLivePoint.Guitar = 0;
                    this.actLivePoint.n現在のLivePoint.Bass = 0;

                    if( this.tx判定画像anime != null && this.txボーナスエフェクト != null )
                    {
                        this.tx判定画像anime.t2D描画( CDTXMania.app.Device, 1280, 720 );
                        this.txボーナスエフェクト.t2D描画( CDTXMania.app.Device, 1280, 720 );
                    }
					base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
					this.actFI.tフェードイン開始();
					base.b初めての進行描画 = false;
				}
				if( CDTXMania.ConfigIni.bSTAGEFAILED有効 && ( base.eフェーズID == CStage.Eフェーズ.共通_通常状態 ) )
				{
					bool bFailedGuitar = this.actGauge.IsFailed( E楽器パート.GUITAR );		// #23630 2011.11.12 yyagi: deleted AutoPlay condition: not to be failed at once
					bool bFailedBass   = this.actGauge.IsFailed( E楽器パート.BASS );		// #23630
					bool bFailedNoChips = (!CDTXMania.DTX.bチップがある.Guitar && !CDTXMania.DTX.bチップがある.Bass);	// #25216 2011.5.21 yyagi add condition
					if ( bFailedGuitar || bFailedBass || bFailedNoChips )						// #25216 2011.5.21 yyagi: changed codition: && -> ||
					{
						this.actStageFailed.Start();
						CDTXMania.DTX.t全チップの再生停止();
						base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED;
					}
				}
				this.t進行描画・背景();
                this.t進行描画・AVI();
				this.t進行描画・MIDIBGM();
				this.t進行描画・パネル文字列();
				this.t進行描画・BGA();
				this.t進行描画・ステータスパネル();
                this.t進行描画・スコア();
				this.t進行描画・レーンフラッシュGB();


				this.t進行描画・DANGER();

                this.t進行描画・グラフ();


				this.t進行描画・WailingBonus();
				this.t進行描画・譜面スクロール速度();
				this.t進行描画・チップアニメ();
                flag = this.t進行描画・チップ(E楽器パート.GUITAR);
                this.t進行描画・RGBボタン();
                this.t進行描画・ギターベース判定ライン();
                this.t進行描画・判定ライン();
				this.t進行描画・判定文字列();
                this.t進行描画・ゲージ();
                this.t進行描画・コンボ();
				this.t進行描画・演奏情報();
				//this.t進行描画・Wailing枠();

                this.t進行描画・チップファイアGB();
				this.t進行描画・STAGEFAILED();
                flag2 = this.t進行描画・フェードイン・アウト();
                if ( flag && (base.eフェーズID == CStage.Eフェーズ.共通_通常状態 ) )
                {
                    this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージクリア;
                    base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト;
                    CDTXMania.Skin.soundステージクリア音.t再生する();
                    this.actFOStageClear.tフェードアウト開始();
                }
				if( flag2 )
				{
                    if (!CDTXMania.Skin.soundステージクリア音.b再生中)
                    {
                        Debug.WriteLine("Total On進行描画=" + sw.ElapsedMilliseconds + "ms");
                        return (int)this.eフェードアウト完了時の戻り値;
                    }
				}
                ManageMixerQueue();

				// キー入力

				if( CDTXMania.act現在入力を占有中のプラグイン == null )
				{
					this.tキー入力();
				}
			}
            base.sw.Stop();
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private CTexture txレーン;
        public CAct演奏Guitarグラフ actGraph;
        public bool bサビ区間;
        public double UnitTime;

		protected override E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, bool bCorrectLane )
		{
			E判定 eJudgeResult = tチップのヒット処理( nHitTime, pChip, E楽器パート.GUITAR, bCorrectLane );
            if (CDTXMania.ConfigIni.nSkillMode == 0)
            {
                this.actGraph.dbグラフ値現在_渡G = CScoreIni.t旧演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, E楽器パート.DRUMS, bIsAutoPlay);
            }
            else if (CDTXMania.ConfigIni.nSkillMode == 1)
            {
                this.actGraph.dbグラフ値現在_渡G = CScoreIni.t演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay);
            }
			return eJudgeResult;
		}
		protected override void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part )
		{
			this.tチップのヒット処理・BadならびにTight時のMiss( part, 0, E楽器パート.GUITAR );
		}
		protected override void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part, int nLane )
		{
			this.tチップのヒット処理・BadならびにTight時のMiss( part, nLane, E楽器パート.GUITAR );
		}

		protected override void t進行描画・AVI()
		{
		    base.t進行描画・AVI( 0, 0 );
		}
		protected override void t進行描画・BGA()
		{
		    //base.t進行描画・BGA( 500, 50 );
		}
		protected override void t進行描画・DANGER()			// #23631 2011.4.19 yyagi
		{
			//this.actDANGER.t進行描画( false, this.actGauge.db現在のゲージ値.Guitar < 0.3, this.actGauge.db現在のゲージ値.Bass < 0.3 );
			this.actDANGER.t進行描画( false, this.actGauge.IsDanger(E楽器パート.GUITAR), this.actGauge.IsDanger(E楽器パート.BASS) );
		}
        private void t進行描画・グラフ()
        {
            if ( CDTXMania.ConfigIni.bGraph.Guitar && CDTXMania.DTX.bチップがある.Guitar && !CDTXMania.DTX.bチップがある.Bass )
            {
                this.actGraph.On進行描画();
                this.actGraph.db現在の判定数合計G = this.nヒット数・Auto含む.Guitar.Perfect + this.nヒット数・Auto含む.Guitar.Great + this.nヒット数・Auto含む.Guitar.Good + this.nヒット数・Auto含む.Guitar.Miss + this.nヒット数・Auto含む.Guitar.Poor;
            }
        }
		protected override void t進行描画・Wailing枠()
		{
			base.t進行描画・Wailing枠( 292, 0x251,
				CDTXMania.ConfigIni.bReverse.Guitar ? 340 : 130,
				CDTXMania.ConfigIni.bReverse.Bass ?   340 : 130
			);
		}
		private void t進行描画・ギターベース判定ライン()	// yyagi: ドラム画面とは座標が違うだけですが、まとめづらかったのでそのまま放置してます。
		{
			if ( ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				if ( CDTXMania.DTX.bチップがある.Guitar )
				{
					int y = ( CDTXMania.ConfigIni.bReverse.Guitar ? 611 : 153 );
					for ( int i = 0; i < 4; i++ )
					{
						if ( this.txヒットバー != null && CDTXMania.ConfigIni.bJudgeLineDisp.Guitar )
						{
							this.txヒットバー.t2D描画( CDTXMania.app.Device, 79, y, new Rectangle( 0, i * 6, 252, 6 ) );
						}
					}
				}
				if ( CDTXMania.DTX.bチップがある.Bass )
				{
					int y = ( CDTXMania.ConfigIni.bReverse.Bass ? 611 : 153 );
					for ( int j = 0; j < 4; j++ )
					{
						if ( this.txヒットバー != null && CDTXMania.ConfigIni.bJudgeLineDisp.Bass )
						{
                            this.txヒットバー.t2D描画(CDTXMania.app.Device, 949, y, new Rectangle(0, j * 6, 252, 6));
						}
					}
				}
			}
		}

		protected override void t進行描画・パネル文字列()
		{
			base.t進行描画・パネル文字列( 0xb5, 430 );
		}

		protected override void t進行描画・演奏情報()
		{
			base.t進行描画・演奏情報( 500, 257 );
		}

        protected override void tJudgeLineMovingUpandDown()
        {

        }

		protected override void ドラムスクロール速度アップ()
		{
            CDTXMania.ConfigIni.n譜面スクロール速度.Guitar = Math.Min(CDTXMania.ConfigIni.n譜面スクロール速度.Guitar + 1, 1999);
		}
		protected override void ドラムスクロール速度ダウン()
		{
            CDTXMania.ConfigIni.n譜面スクロール速度.Guitar = Math.Max(CDTXMania.ConfigIni.n譜面スクロール速度.Guitar - 1, 0);
		}

		protected override void t入力処理・ドラム()
		{
			// ギタレボモードでは何もしない
		}

		protected override void t背景テクスチャの生成()
		{
			Rectangle bgrect = new Rectangle( 0, 0, 1280, 720 );
			string DefaultBgFilename = @"Graphics\7_background_Guitar.jpg";
			string BgFilename = "";
			string BACKGROUND = null;
			if ( ( CDTXMania.DTX.BACKGROUND_GR != null ) && ( CDTXMania.DTX.BACKGROUND_GR.Length > 0 ) )
			{
				BACKGROUND = CDTXMania.DTX.BACKGROUND_GR;
			}
			else if ( ( CDTXMania.DTX.BACKGROUND != null ) && ( CDTXMania.DTX.BACKGROUND.Length > 0 ) )
			{
				BACKGROUND = CDTXMania.DTX.BACKGROUND;
			}
			if ( ( BACKGROUND != null ) && ( BACKGROUND.Length > 0 ) )
			{
				BgFilename = CDTXMania.DTX.strフォルダ名 + BACKGROUND;
			}
			base.t背景テクスチャの生成( DefaultBgFilename, bgrect, BgFilename );
		}

		protected override void t進行描画・チップ・ドラムス( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			// int indexSevenLanes = this.nチャンネル0Atoレーン07[ pChip.nチャンネル番号 - 0x11 ];
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
				pChip.bHit = true;
                this.tサウンド再生(pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, E楽器パート.DRUMS, dTX.nモニタを考慮した音量(E楽器パート.DRUMS));
			}
		}
		protected override void t進行描画・チップ・ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst )
		{
			base.t進行描画・チップ・ギターベース( configIni, ref dTX, ref pChip, inst,
				164, 612, 104, 670, 0, 0, 0, 11, 196, 10, 38, 38, 1000, 1000, 1000, 38, 38 );
		}
#if false
		protected override void t進行描画・チップ・ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst )
		{
			int instIndex = (int) inst;
			if ( configIni.bGuitar有効 )
			{
				if ( configIni.bSudden[instIndex ] )
				{
					pChip.b可視 = pChip.nバーからの距離dot[ instIndex ] < 200;
				}
				if ( configIni.bHidden[ instIndex ] && ( pChip.nバーからの距離dot[ instIndex ] < 100 ) )
				{
					pChip.b可視 = false;
				}

				bool bChipHasR = ( ( pChip.nチャンネル番号 & 4 ) > 0 );
				bool bChipHasG = ( ( pChip.nチャンネル番号 & 2 ) > 0 );
				bool bChipHasB = ( ( pChip.nチャンネル番号 & 1 ) > 0 );
				bool bChipHasW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
				bool bChipIsO  = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x00 );

				int OPEN = ( inst == E楽器パート.GUITAR ) ? 0x20 : 0xA0;
				if ( !pChip.bHit && pChip.b可視 )
				{
					int y = configIni.bReverse[ instIndex ] ? ( 369 - pChip.nバーからの距離dot[ instIndex ]) : ( 40 + pChip.nバーからの距離dot[ instIndex ] );
					if ( ( y > 0 ) && ( y < 409 ) )
					{
						if ( this.txチップ != null )
						{
							int nアニメカウンタ現在の値 = this.ctチップ模様アニメ[ instIndex ].n現在の値;
							if ( pChip.nチャンネル番号 == OPEN )
							{
								{
									int xo = ( inst == E楽器パート.GUITAR ) ? 26 : 480;
									this.txチップ.t2D描画( CDTXMania.app.Device, xo, y - 4, new Rectangle( 0, 192 + ( ( nアニメカウンタ現在の値 % 5 ) * 8 ), 103, 8 ) );
								}
							}
							Rectangle rc = new Rectangle( 0, nアニメカウンタ現在の値 * 8, 32, 8 );
							int x;
							if ( inst == E楽器パート.GUITAR )
							{
								x = ( configIni.bLeft.Guitar ) ? 98 : 26;
							}
							else
							{
								x = ( configIni.bLeft.Bass ) ? 552 : 480;
							}
							int deltaX = ( configIni.bLeft[ instIndex ] ) ? -36 : +36; 
							if ( bChipHasR )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, x, y - 4, rc );
							}
							rc.X += 32;
							if ( bChipHasG )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, x, y - 4, rc );
							}
							rc.X += 32;
							if ( bChipHasB )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, x, y - 4, rc );
							}
						}
					}
				}
				// if ( ( configIni.bAutoPlay.Guitar && !pChip.bHit ) && ( pChip.nバーからの距離dot.Guitar < 0 ) )
				if ( ( !pChip.bHit ) && ( pChip.nバーからの距離dot[ instIndex ] < 0 ) )
				{
					int lo = ( inst == E楽器パート.GUITAR ) ? 0 : 3;	// lane offset
					bool autoR = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtR : bIsAutoPlay.BsR;
					bool autoG = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtG : bIsAutoPlay.BsG;
					bool autoB = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtB : bIsAutoPlay.BsB;
					if ( ( bChipHasR || bChipIsO ) && autoR )
					{
						this.actChipFireGB.Start( 0 + lo );
					}
					if ( ( bChipHasG || bChipIsO ) && autoG )
					{
						this.actChipFireGB.Start( 1 + lo );
					}
					if ( ( bChipHasB || bChipIsO ) && autoB )
					{
						this.actChipFireGB.Start( 2 + lo );
					}
					if ( ( inst == E楽器パート.GUITAR && bIsAutoPlay.GtPick ) || ( inst == E楽器パート.BASS && bIsAutoPlay.BsPick ) )
					{
						bool pushingR = CDTXMania.Pad.b押されている( inst, Eパッド.R );
						bool pushingG = CDTXMania.Pad.b押されている( inst, Eパッド.G );
						bool pushingB = CDTXMania.Pad.b押されている( inst, Eパッド.B );
						bool bMiss = true;
						if ( ( ( bChipIsO == true ) && ( !pushingR | autoR ) && ( !pushingG | autoG ) && ( !pushingB | autoB ) ) ||
							( ( bChipHasR == ( pushingR | autoR ) ) && ( bChipHasG == ( pushingG | autoG ) ) && ( bChipHasB == ( pushingB | autoB ) ) )
						)
						{
							bMiss = false;
						}
						pChip.bHit = true;
						this.tサウンド再生( pChip, CDTXMania.Timer.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, inst, dTX.nモニタを考慮した音量( inst ) );
						this.r次にくるギターChip = null;
						this.tチップのヒット処理( pChip.n発声時刻ms, pChip );
					}
				}
				// break;
				return;
			}
			if ( !pChip.bHit && ( pChip.nバーからの距離dot[ instIndex ] < 0 ) )
			{
				pChip.bHit = true;
				this.tサウンド再生( pChip, CDTXMania.Timer.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, inst, dTX.nモニタを考慮した音量( inst ) );
			}
		}
#endif
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

				//
				// 後日、以下の部分を何とかCStage演奏画面共通.csに移したい。
				//
				if ( !pChip.bHit && pChip.b可視 )
				{
					int[] y_base = { 154, 611 };			// ドラム画面かギター画面かで変わる値
					int offset = 0;						// ドラム画面かギター画面かで変わる値

					const int WailingWidth = 54;		// 4種全て同じ値
					const int WailingHeight = 68;		// 4種全て同じ値
					const int baseTextureOffsetX = 0;	// ドラム画面かギター画面かで変わる値
					const int baseTextureOffsetY = 22;	// ドラム画面かギター画面かで変わる値
					const int drawX = 287;				// 4種全て異なる値

					const int numA = 34;				// 4種全て同じ値;
					int y = configIni.bReverse.Guitar ? ( y_base[ 1 ] - pChip.nバーからの距離dot.Guitar ) : ( y_base[ 0 ] + pChip.nバーからの距離dot.Guitar );
					int numB = y - offset;				// 4種全て同じ定義
					int numC = 0;						// 4種全て同じ初期値
					const int numD = 709;				// ドラム画面かギター画面かで変わる値
					if ( ( numB < ( numD + numA ) ) && ( numB > -numA ) )	// 以下のロジックは4種全て同じ
					{
						int c = this.ctWailingチップ模様アニメ.n現在の値;
						Rectangle rect = new Rectangle( baseTextureOffsetX, baseTextureOffsetY, WailingWidth, WailingHeight );
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
				//    if( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
				//    {
				//        pChip.bHit = true;
				//        if( configIni.bAutoPlay.Guitar )
				//        {
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
			}
#if TEST_NOTEOFFMODE	// 2011.1.1 yyagi TEST
			switch ( pChip.n整数値 )
			{
				case 0x04:	// HH消音あり(従来同等)
					CDTXMania.DTX.b演奏で直前の音を消音する.HH = true;
					break;
				case 0x05:	// HH消音無し
					CDTXMania.DTX.b演奏で直前の音を消音する.HH = false;
					break;
				case 0x06:	// ギター消音あり(従来同等)
					CDTXMania.DTX.b演奏で直前の音を消音する.Guitar = true;
					break;
				case 0x07:	// ギター消音無し
					CDTXMania.DTX.b演奏で直前の音を消音する.Guitar = false;
					break;
				case 0x08:	// ベース消音あり(従来同等)
					CDTXMania.DTX.b演奏で直前の音を消音する.Bass = true;
					break;
				case 0x09:	// ベース消音無し
					CDTXMania.DTX.b演奏で直前の音を消音する.Bass = false;
					break;
			}
#endif

		}
        protected override void t進行描画・チップ・ボーナス(CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip)
        {
            if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
            {
                pChip.bHit = true;
            }
        }
#if false
		protected override void t進行描画・チップ・ベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( configIni.bGuitar有効 )
			{
				if ( configIni.bSudden.Bass )
				{
					pChip.b可視 = pChip.nバーからの距離dot.Bass < 200;
				}
				if ( configIni.bHidden.Bass && ( pChip.nバーからの距離dot.Bass < 100 ) )
				{
					pChip.b可視 = false;
				}
				if ( !pChip.bHit && pChip.b可視 )
				{
					int num8 = configIni.bReverse.Bass ? ( 0x171 - pChip.nバーからの距離dot.Bass ) : ( 40 + pChip.nバーからの距離dot.Bass );
					if ( ( num8 > 0 ) && ( num8 < 0x199 ) )
					{
						int num9 = this.ctチップ模様アニメ.Bass.n現在の値;
						if ( pChip.nチャンネル番号 == 160 )
						{
							if ( this.txチップ != null )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 480, num8 - 4, new Rectangle( 0, 0xc0 + ( ( num9 % 5 ) * 8 ), 0x67, 8 ) );
							}
						}
						else if ( !configIni.bLeft.Bass )
						{
							Rectangle rectangle3 = new Rectangle( 0, num9 * 8, 0x20, 8 );
							if ( ( ( pChip.nチャンネル番号 & 4 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 480, num8 - 4, rectangle3 );
							}
							rectangle3.X += 0x20;
							if ( ( ( pChip.nチャンネル番号 & 2 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 0x204, num8 - 4, rectangle3 );
							}
							rectangle3.X += 0x20;
							if ( ( ( pChip.nチャンネル番号 & 1 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 0x228, num8 - 4, rectangle3 );
							}
						}
						else
						{
							Rectangle rectangle4 = new Rectangle( 0, num9 * 8, 0x20, 8 );
							if ( ( ( pChip.nチャンネル番号 & 4 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 0x228, num8 - 4, rectangle4 );
							}
							rectangle4.X += 0x20;
							if ( ( ( pChip.nチャンネル番号 & 2 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 0x204, num8 - 4, rectangle4 );
							}
							rectangle4.X += 0x20;
							if ( ( ( pChip.nチャンネル番号 & 1 ) != 0 ) && ( this.txチップ != null ) )
							{
								this.txチップ.t2D描画( CDTXMania.app.Device, 480, num8 - 4, rectangle4 );
							}
						}
					}
				}
				if ( ( configIni.bAutoPlay.Bass && !pChip.bHit ) && ( pChip.nバーからの距離dot.Bass < 0 ) )
				{
					pChip.bHit = true;
					if ( ( ( pChip.nチャンネル番号 & 4 ) != 0 ) || ( pChip.nチャンネル番号 == 0xA0 ) )
					{
						this.actChipFireGB.Start( 3 );
					}
					if ( ( ( pChip.nチャンネル番号 & 2 ) != 0 ) || ( pChip.nチャンネル番号 == 0xA0 ) )
					{
						this.actChipFireGB.Start( 4 );
					}
					if ( ( ( pChip.nチャンネル番号 & 1 ) != 0 ) || ( pChip.nチャンネル番号 == 0xA0 ) )
					{
						this.actChipFireGB.Start( 5 );
					}
					this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, E楽器パート.BASS, dTX.nモニタを考慮した音量( E楽器パート.BASS ) );
					this.r次にくるベースChip = null;
					this.tチップのヒット処理( pChip.n発声時刻ms, pChip );
				}
				return;
			}
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
			{
				pChip.bHit = true;
				this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, E楽器パート.BASS, dTX.nモニタを考慮した音量( E楽器パート.BASS ) );
			}
		}
#endif
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
					int[] y_base = { 154, 369 };			// ドラム画面かギター画面かで変わる値
					int offset = 0;						// ドラム画面かギター画面かで変わる値

					const int WailingWidth = 54;		// 4種全て同じ値
					const int WailingHeight = 68;		// 4種全て同じ値
					const int baseTextureOffsetX = 0;	// ドラム画面かギター画面かで変わる値
					const int baseTextureOffsetY = 22;	// ドラム画面かギター画面かで変わる値
					const int drawX = 1155;				// 4種全て異なる値

					const int numA = 34;				// 4種全て同じ値
					int y = CDTXMania.ConfigIni.bReverse.Bass ? ( y_base[ 1 ] - pChip.nバーからの距離dot.Bass ) : ( y_base[ 0 ] + pChip.nバーからの距離dot.Bass );
					int numB = y - offset;				// 4種全て同じ定義
					int numC = 0;						// 4種全て同じ初期値
					const int numD = 709;				// ドラム画面かギター画面かで変わる値
					if ( ( numB < ( numD + numA ) ) && ( numB > -numA ) )	// 以下のロジックは4種全て同じ
					{
						int c = this.ctWailingチップ模様アニメ.n現在の値;
                        Rectangle rect = new Rectangle(baseTextureOffsetX, baseTextureOffsetY, WailingWidth, WailingHeight);
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
                            this.txチップ.t2D描画(CDTXMania.app.Device, drawX, (y - numA) + numC, rect);
						}
					}
				}
				//    if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
				//    {
				//        pChip.bHit = true;
				//        if ( configIni.bAutoPlay.Bass )
				//        {
				//            this.actWailingBonus.Start( E楽器パート.BASS, this.r現在の歓声Chip.Bass );
				//        }
				//    }
				//    return;
				//}
				//pChip.bHit = true;
				base.t進行描画・チップ・ベース・ウェイリング( configIni, ref dTX, ref pChip );
			}
		}
		protected override void t進行描画・チップ・空打ち音設定・ドラム( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
				pChip.bHit = true;
			}
		}
		protected override void t進行描画・チップ・小節線( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			int n小節番号plus1 = pChip.n発声位置 / 0x180;
			if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
			{
				pChip.bHit = true;
				this.actPlayInfo.n小節番号 = n小節番号plus1 - 1;
				if ( configIni.bWave再生位置自動調整機能有効 && bIsDirectSound )
				{
					dTX.tWave再生位置自動補正();
				}
			}
			if ( ( pChip.b可視 && configIni.bGuitar有効 ))
			{
				int y = CDTXMania.ConfigIni.bReverse.Guitar ? ( ( 611 - pChip.nバーからの距離dot.Guitar ) + 0 ) : ( ( 154 + pChip.nバーからの距離dot.Guitar ) + 9 );
				if ( ( dTX.bチップがある.Guitar && ( y > 104 ) ) && ( ( y < 670 ) && ( this.txチップ != null ) ) )
				{
                    if( configIni.bJudgeLineDisp.Guitar )
					    this.txチップ.t2D描画( CDTXMania.app.Device, 88, y, new Rectangle( 0, 20, 193, 2 ) );
				}
				y = CDTXMania.ConfigIni.bReverse.Bass ? ( ( 611 - pChip.nバーからの距離dot.Bass ) + 0 ) : ( ( 154 + pChip.nバーからの距離dot.Bass ) + 9 );
				if ( ( dTX.bチップがある.Bass && ( y > 104 ) ) && ( ( y < 670 ) && ( this.txチップ != null ) ) )
				{
                    if( configIni.bJudgeLineDisp.Bass )
					    this.txチップ.t2D描画( CDTXMania.app.Device, 959, y, new Rectangle( 0, 20, 193, 2 ) );
				}
			}

		}
		#endregion
	}
}
