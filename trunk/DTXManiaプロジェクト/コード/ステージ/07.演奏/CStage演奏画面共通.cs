using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;
using DirectShowLib;
using FDK;

namespace DTXMania
{
	/// <summary>
	/// 演奏画面の共通クラス (ドラム演奏画面, ギター演奏画面の継承元)
	/// </summary>
	internal abstract class CStage演奏画面共通 : CStage
	{
		// プロパティ

		public bool bAUTOでないチップが１つでもバーを通過した
		{
			get;
			protected set;
		}

        // メソッド

        #region [ t演奏結果を格納する・ドラム() ]
        public void t演奏結果を格納する・ドラム(out CScoreIni.C演奏記録 Drums)
        {
            Drums = new CScoreIni.C演奏記録();

            if (CDTXMania.DTX.bチップがある.Drums && !CDTXMania.ConfigIni.bギタレボモード)
            {
                Drums.nスコア = (long)this.actScore.Get(E楽器パート.DRUMS);
                if (CDTXMania.ConfigIni.nSkillMode == 0)
                {
                    Drums.dbゲーム型スキル値 = CScoreIni.t旧ゲーム型スキルを計算して返す(CDTXMania.DTX.LEVEL.Drums, CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay);
                    Drums.db演奏型スキル値 = CScoreIni.t旧演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, E楽器パート.DRUMS, bIsAutoPlay);
                }
                else if (CDTXMania.ConfigIni.nSkillMode == 1)
                {
                    Drums.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す(CDTXMania.DTX.LEVEL.Drums, CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay);
                    Drums.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数・Auto含まない.Drums.Perfect, this.nヒット数・Auto含まない.Drums.Great, this.nヒット数・Auto含まない.Drums.Good, this.nヒット数・Auto含まない.Drums.Poor, this.nヒット数・Auto含まない.Drums.Miss, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay);
                }
                Drums.nPerfect数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数・Auto含む.Drums.Perfect : this.nヒット数・Auto含まない.Drums.Perfect;
                Drums.nGreat数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数・Auto含む.Drums.Great : this.nヒット数・Auto含まない.Drums.Great;
                Drums.nGood数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数・Auto含む.Drums.Good : this.nヒット数・Auto含まない.Drums.Good;
                Drums.nPoor数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数・Auto含む.Drums.Poor : this.nヒット数・Auto含まない.Drums.Poor;
                Drums.nMiss数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数・Auto含む.Drums.Miss : this.nヒット数・Auto含まない.Drums.Miss;
                Drums.nPerfect数・Auto含まない = this.nヒット数・Auto含まない.Drums.Perfect;
                Drums.nGreat数・Auto含まない = this.nヒット数・Auto含まない.Drums.Great;
                Drums.nGood数・Auto含まない = this.nヒット数・Auto含まない.Drums.Good;
                Drums.nPoor数・Auto含まない = this.nヒット数・Auto含まない.Drums.Poor;
                Drums.nMiss数・Auto含まない = this.nヒット数・Auto含まない.Drums.Miss;
                Drums.n最大コンボ数 = this.actCombo.n現在のコンボ数.Drums最高値;
                Drums.n全チップ数 = CDTXMania.DTX.n可視チップ数.Drums;
                for (int i = 0; i < (int)Eレーン.MAX; i++)
                {
                    Drums.bAutoPlay[i] = bIsAutoPlay[i];
                }
                Drums.bTight = CDTXMania.ConfigIni.bTight;
                for (int i = 0; i < 3; i++)
                {
                    Drums.bSudden[i] = CDTXMania.ConfigIni.bSudden[i];
                    Drums.bHidden[i] = CDTXMania.ConfigIni.bHidden[i];
                    Drums.bReverse[i] = CDTXMania.ConfigIni.bReverse[i];
                    Drums.eRandom[i] = CDTXMania.ConfigIni.eRandom[i];
                    Drums.bLight[i] = CDTXMania.ConfigIni.bLight[i];
                    Drums.bLeft[i] = CDTXMania.ConfigIni.bLeft[i];
                    Drums.f譜面スクロール速度[i] = ((float)(CDTXMania.ConfigIni.n譜面スクロール速度[i] + 1)) * 0.5f;
                }
                Drums.eDark = CDTXMania.ConfigIni.eDark;
                Drums.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
                Drums.n演奏速度分母 = 20;
                Drums.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
                Drums.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
                Drums.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
                Drums.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
                Drums.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
                Drums.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
                Drums.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
                Drums.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
                Drums.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
                Drums.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
                Drums.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Drums;
                Drums.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Drums;
                Drums.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Drums;
                Drums.b演奏にマウスを使用した = this.b演奏にマウスを使った.Drums;
                Drums.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
                Drums.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
                Drums.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
                Drums.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
                Drums.strDTXManiaのバージョン = CDTXMania.VERSION;
                Drums.最終更新日時 = DateTime.Now.ToString();
                Drums.Hash = CScoreIni.t演奏セクションのMD5を求めて返す(Drums);
            }
        }
        #endregion
        #region [ t演奏結果を格納する・ギター() ]
        public void t演奏結果を格納する・ギター(out CScoreIni.C演奏記録 Guitar)
        {
            Guitar = new CScoreIni.C演奏記録();

            if (CDTXMania.DTX.bチップがある.Guitar)
            {
                Guitar.nスコア = (long)this.actScore.Get(E楽器パート.GUITAR);
                Guitar.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す(CDTXMania.DTX.LEVEL.Guitar, CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数・Auto含まない.Guitar.Perfect, this.nヒット数・Auto含まない.Guitar.Great, this.actCombo.n現在のコンボ数.Guitar最高値, E楽器パート.GUITAR, bIsAutoPlay);
                Guitar.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数・Auto含まない.Guitar.Perfect, this.nヒット数・Auto含まない.Guitar.Great, this.nヒット数・Auto含まない.Guitar.Good, this.nヒット数・Auto含まない.Guitar.Poor, this.nヒット数・Auto含まない.Guitar.Miss, this.actCombo.n現在のコンボ数.Guitar最高値, E楽器パート.GUITAR, bIsAutoPlay);
                Guitar.nPerfect数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数・Auto含む.Guitar.Perfect : this.nヒット数・Auto含まない.Guitar.Perfect;
                Guitar.nGreat数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数・Auto含む.Guitar.Great : this.nヒット数・Auto含まない.Guitar.Great;
                Guitar.nGood数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数・Auto含む.Guitar.Good : this.nヒット数・Auto含まない.Guitar.Good;
                Guitar.nPoor数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数・Auto含む.Guitar.Poor : this.nヒット数・Auto含まない.Guitar.Poor;
                Guitar.nMiss数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数・Auto含む.Guitar.Miss : this.nヒット数・Auto含まない.Guitar.Miss;
                Guitar.nPerfect数・Auto含まない = this.nヒット数・Auto含まない.Guitar.Perfect;
                Guitar.nGreat数・Auto含まない = this.nヒット数・Auto含まない.Guitar.Great;
                Guitar.nGood数・Auto含まない = this.nヒット数・Auto含まない.Guitar.Good;
                Guitar.nPoor数・Auto含まない = this.nヒット数・Auto含まない.Guitar.Poor;
                Guitar.nMiss数・Auto含まない = this.nヒット数・Auto含まない.Guitar.Miss;
                Guitar.n最大コンボ数 = this.actCombo.n現在のコンボ数.Guitar最高値;
                Guitar.n全チップ数 = CDTXMania.DTX.n可視チップ数.Guitar;
                for (int i = 0; i < (int)Eレーン.MAX; i++)
                {
                    Guitar.bAutoPlay[i] = bIsAutoPlay[i];
                }
                Guitar.bTight = CDTXMania.ConfigIni.bTight;
                for (int i = 0; i < 3; i++)
                {
                    Guitar.bSudden[i] = CDTXMania.ConfigIni.bSudden[i];
                    Guitar.bHidden[i] = CDTXMania.ConfigIni.bHidden[i];
                    Guitar.bReverse[i] = CDTXMania.ConfigIni.bReverse[i];
                    Guitar.eRandom[i] = CDTXMania.ConfigIni.eRandom[i];
                    Guitar.bLight[i] = CDTXMania.ConfigIni.bLight[i];
                    Guitar.bLeft[i] = CDTXMania.ConfigIni.bLeft[i];
                    Guitar.f譜面スクロール速度[i] = ((float)(CDTXMania.ConfigIni.n譜面スクロール速度[i] + 1)) * 0.5f;
                }
                Guitar.eDark = CDTXMania.ConfigIni.eDark;
                Guitar.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
                Guitar.n演奏速度分母 = 20;
                Guitar.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
                Guitar.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
                Guitar.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
                Guitar.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
                Guitar.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
                Guitar.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
                Guitar.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
                Guitar.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
                Guitar.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
                Guitar.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
                Guitar.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Guitar;
                Guitar.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Guitar;
                Guitar.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Guitar;
                Guitar.b演奏にマウスを使用した = this.b演奏にマウスを使った.Guitar;
                Guitar.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
                Guitar.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
                Guitar.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
                Guitar.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
                Guitar.strDTXManiaのバージョン = CDTXMania.VERSION;
                Guitar.最終更新日時 = DateTime.Now.ToString();
                Guitar.Hash = CScoreIni.t演奏セクションのMD5を求めて返す(Guitar);
            }
        }
        #endregion
        #region [ t演奏結果を格納する・ベース() ]
        public void t演奏結果を格納する・ベース(out CScoreIni.C演奏記録 Bass)
        {
            Bass = new CScoreIni.C演奏記録();

            if (CDTXMania.DTX.bチップがある.Bass)
            {
                Bass.nスコア = (long)this.actScore.Get(E楽器パート.BASS);
                Bass.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す(CDTXMania.DTX.LEVEL.Bass, CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数・Auto含まない.Bass.Perfect, this.nヒット数・Auto含まない.Bass.Great, this.actCombo.n現在のコンボ数.Bass最高値, E楽器パート.BASS, bIsAutoPlay);
                Bass.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す(CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数・Auto含まない.Bass.Perfect, this.nヒット数・Auto含まない.Bass.Great, this.nヒット数・Auto含まない.Bass.Good, this.nヒット数・Auto含まない.Bass.Poor, this.nヒット数・Auto含まない.Bass.Miss, this.actCombo.n現在のコンボ数.Bass最高値, E楽器パート.BASS, bIsAutoPlay);
                Bass.nPerfect数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数・Auto含む.Bass.Perfect : this.nヒット数・Auto含まない.Bass.Perfect;
                Bass.nGreat数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数・Auto含む.Bass.Great : this.nヒット数・Auto含まない.Bass.Great;
                Bass.nGood数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数・Auto含む.Bass.Good : this.nヒット数・Auto含まない.Bass.Good;
                Bass.nPoor数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数・Auto含む.Bass.Poor : this.nヒット数・Auto含まない.Bass.Poor;
                Bass.nMiss数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数・Auto含む.Bass.Miss : this.nヒット数・Auto含まない.Bass.Miss;
                Bass.nPerfect数・Auto含まない = this.nヒット数・Auto含まない.Bass.Perfect;
                Bass.nGreat数・Auto含まない = this.nヒット数・Auto含まない.Bass.Great;
                Bass.nGood数・Auto含まない = this.nヒット数・Auto含まない.Bass.Good;
                Bass.nPoor数・Auto含まない = this.nヒット数・Auto含まない.Bass.Poor;
                Bass.nMiss数・Auto含まない = this.nヒット数・Auto含まない.Bass.Miss;
                Bass.n最大コンボ数 = this.actCombo.n現在のコンボ数.Bass最高値;
                Bass.n全チップ数 = CDTXMania.DTX.n可視チップ数.Bass;
                for (int i = 0; i < (int)Eレーン.MAX; i++)
                {
                    Bass.bAutoPlay[i] = bIsAutoPlay[i];
                }
                Bass.bTight = CDTXMania.ConfigIni.bTight;
                for (int i = 0; i < 3; i++)
                {
                    Bass.bSudden[i] = CDTXMania.ConfigIni.bSudden[i];
                    Bass.bHidden[i] = CDTXMania.ConfigIni.bHidden[i];
                    Bass.bReverse[i] = CDTXMania.ConfigIni.bReverse[i];
                    Bass.eRandom[i] = CDTXMania.ConfigIni.eRandom[i];
                    Bass.bLight[i] = CDTXMania.ConfigIni.bLight[i];
                    Bass.bLeft[i] = CDTXMania.ConfigIni.bLeft[i];
                    Bass.f譜面スクロール速度[i] = ((float)(CDTXMania.ConfigIni.n譜面スクロール速度[i] + 1)) * 0.5f;
                }
                Bass.eDark = CDTXMania.ConfigIni.eDark;
                Bass.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
                Bass.n演奏速度分母 = 20;
                Bass.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
                Bass.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
                Bass.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
                Bass.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
                Bass.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
                Bass.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
                Bass.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
                Bass.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
                Bass.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
                Bass.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
                Bass.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Bass;			// #24280 2011.1.29 yyagi
                Bass.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Bass;				//
                Bass.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Bass;		//
                Bass.b演奏にマウスを使用した = this.b演奏にマウスを使った.Bass;					//
                Bass.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
                Bass.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
                Bass.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
                Bass.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
                Bass.strDTXManiaのバージョン = CDTXMania.VERSION;
                Bass.最終更新日時 = DateTime.Now.ToString();
                Bass.Hash = CScoreIni.t演奏セクションのMD5を求めて返す(Bass);
            }
        }
        #endregion

		// CStage 実装

		public override void On活性化()
		{
            listChip = CDTXMania.DTX.listChip;
            listWAV = CDTXMania.DTX.listWAV;

			this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.継続;
            this.n現在のトップChip = (listChip.Count > 0) ? 0 : -1;
			this.L最後に再生したHHの実WAV番号 = new List<int>( 16 );
			this.n最後に再生したHHのチャンネル番号 = 0;
			this.n最後に再生した実WAV番号.Guitar = -1;
			this.n最後に再生した実WAV番号.Bass = -1;
			for ( int i = 0; i < 50; i++ )
			{
				this.n最後に再生したBGMの実WAV番号[ i ] = -1;
			}
			this.r次にくるギターChip = null;
			this.r次にくるベースChip = null;
			for ( int j = 0; j < 12; j++ )
			{
				this.r現在の空うちドラムChip[ j ] = null;
			}
			this.r現在の空うちギターChip = null;
			this.r現在の空うちベースChip = null;
			for ( int k = 0; k < 3; k++ )
			{
				//for ( int n = 0; n < 5; n++ )
				//{
					this.nヒット数・Auto含まない[ k ] = new CHITCOUNTOFRANK();
					this.nヒット数・Auto含む[ k ] = new CHITCOUNTOFRANK();
				//}
				this.queWailing[ k ] = new Queue<CDTX.CChip>();
				this.r現在の歓声Chip[ k ] = null;
			}
			for ( int i = 0; i < 3; i++ )
			{
				this.b演奏にキーボードを使った[ i ] = false;
				this.b演奏にジョイパッドを使った[ i ] = false;
				this.b演奏にMIDI入力を使った[ i ] = false;
				this.b演奏にマウスを使った[ i ] = false;
                this.ctタイマー[i] = null;//new CCounter(0, 3000, 1, CDTXMania.Timer);
			}
			this.bAUTOでないチップが１つでもバーを通過した = false;
			base.On活性化();
			this.tステータスパネルの選択();
			this.tパネル文字列の設定();
            this.nJudgeLinePosY = 561 - CDTXMania.ConfigIni.nJudgeLine;
            this.nShutterInPosY = CDTXMania.ConfigIni.nShutterInSide;
            this.nShutterOutPosY = CDTXMania.ConfigIni.nShutterOutSide;
            this.actJudgeString.iP_A = this.nJudgeLinePosY - 0xbd;
            this.actJudgeString.iP_B = this.nJudgeLinePosY + 0x17;

			this.nInputAdjustTimeMs.Drums = CDTXMania.ConfigIni.nInputAdjustTimeMs.Drums;		// #23580 2011.1.3 yyagi
			this.nInputAdjustTimeMs.Guitar = CDTXMania.ConfigIni.nInputAdjustTimeMs.Guitar;		//        2011.1.7 ikanick 修正
			this.nInputAdjustTimeMs.Bass = CDTXMania.ConfigIni.nInputAdjustTimeMs.Bass;			//
			this.bIsAutoPlay = CDTXMania.ConfigIni.bAutoPlay;									// #24239 2011.1.23 yyagi
			//this.bIsAutoPlay.Guitar = CDTXMania.ConfigIni.bギターが全部オートプレイである;
			//this.bIsAutoPlay.Bass = CDTXMania.ConfigIni.bベースが全部オートプレイである;										// #23559 2011.7.28 yyagi
			actGauge.Init( CDTXMania.ConfigIni.nRisky );									// #23559 2011.7.28 yyagi

            this.nPolyphonicSounds = CDTXMania.ConfigIni.nPoliphonicSounds;

            CDTXMania.Skin.tRemoveMixerAll();	// 効果音のストリームをミキサーから解除しておく

            //lockmixer = new object();
            queueMixerSound = new Queue<stmixer>(64);
            bIsDirectSound = (CDTXMania.Sound管理.GetCurrentSoundDeviceType() == "DirectSound");

			#region [ 演奏開始前にmixer登録しておくべきサウンド(開幕してすぐに鳴らすことになるチップ音)を登録しておく ]
			foreach ( CDTX.CChip pChip in listChip )
			{
//				Debug.WriteLine( "CH=" + pChip.nチャンネル番号.ToString( "x2" ) + ", 整数値=" + pChip.n整数値 +  ", time=" + pChip.n発声時刻ms );
				if ( pChip.n発声時刻ms <= 0 )
				{
					if ( pChip.nチャンネル番号 == 0xDA )
					{
						pChip.bHit = true;
//						Debug.WriteLine( "first [DA] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString( "x2" ) + ", wav=" + pChip.n整数値 + ", time=" + pChip.n発声時刻ms );
						if ( listWAV.ContainsKey( pChip.n整数値・内部番号 ) )
						{
							CDTX.CWAV wc = listWAV[ pChip.n整数値・内部番号 ];
							for ( int i = 0; i < nPolyphonicSounds; i++ )
							{
								if ( wc.rSound[ i ] != null )
								{
									CDTXMania.Sound管理.AddMixer( wc.rSound[ i ], db再生速度 );
									//AddMixer( wc.rSound[ i ] );		// 最初はqueueを介さず直接ミキサー登録する
								}
							}
						}
					}
				}
				else
				{
					break;
				}
			}
			#endregion
			if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )	// #24063 2011.1.24 yyagi Gt/Bsの譜面情報入れ替え
			{
				CDTXMania.DTX.SwapGuitarBassInfos();
			} 

            this.bブーストボーナス = false;
			this.sw = new Stopwatch();
			this.sw2 = new Stopwatch();
//			this.gclatencymode = GCSettings.LatencyMode;
//          GCSettings.LatencyMode = GCLatencyMode.Batch; // 演奏画面中はGCを抑止する
		}
		public override void On非活性化()
		{
			this.L最後に再生したHHの実WAV番号.Clear();	// #23921 2011.1.4 yyagi
			this.L最後に再生したHHの実WAV番号 = null;	//
			for ( int i = 0; i < 3; i++ )
			{
				this.queWailing[ i ].Clear();
				this.queWailing[ i ] = null;
			}
			this.ctWailingチップ模様アニメ = null;
            this.ctBPMバー = null;
			this.ctチップ模様アニメ.Drums = null;
			this.ctチップ模様アニメ.Guitar = null;
			this.ctチップ模様アニメ.Bass = null;
            //listWAV.Clear();
            listWAV = null;
            listChip = null;
            queueMixerSound.Clear();
            queueMixerSound = null;
//          GCSettings.LatencyMode = this.gclatencymode;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
				this.t背景テクスチャの生成();

				this.txWailing枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay wailing cursor.png" ) );


				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );

				CDTXMania.tテクスチャの解放( ref this.txWailing枠 );
				base.OnManagedリソースの解放();
			}
		}

		// その他

		#region [ protected ]
		//-----------------
		public class CHITCOUNTOFRANK
		{
			// Fields
			public int Good;
			public int Great;
			public int Miss;
			public int Perfect;
			public int Poor;

			// Properties
			public int this[ int index ]
			{
				get
				{
					switch ( index )
					{
						case 0:
							return this.Perfect;

						case 1:
							return this.Great;

						case 2:
							return this.Good;

						case 3:
							return this.Poor;

						case 4:
							return this.Miss;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch ( index )
					{
						case 0:
							this.Perfect = value;
							return;

						case 1:
							this.Great = value;
							return;

						case 2:
							this.Good = value;
							return;

						case 3:
							this.Poor = value;
							return;

						case 4:
							this.Miss = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

        static CStage演奏画面共通()
        {
            nJudgeLineMinPosY = 461;//(CDTXMania.ConfigIni.bReverse.Drums ? 259 : 461);
            nJudgeLineMaxPosY = 561;//(CDTXMania.ConfigIni.bReverse.Drums ? 159 : 561);
            nShutterMinPosY = 0;
            nShutterMaxPosY = 100;
        }

		[StructLayout( LayoutKind.Sequential )]
		protected struct STKARAUCHI
		{
			public CDTX.CChip HH;
			public CDTX.CChip SD;
			public CDTX.CChip BD;
			public CDTX.CChip HT;
			public CDTX.CChip LT;
			public CDTX.CChip FT;
			public CDTX.CChip CY;
			public CDTX.CChip HHO;
			public CDTX.CChip RD;
			public CDTX.CChip LC;
            public CDTX.CChip LP;
            public CDTX.CChip LBD;
			public CDTX.CChip this[ int index ]
			{
				get
				{
					switch ( index )
					{
						case 0:
							return this.HH;

						case 1:
							return this.SD;

						case 2:
							return this.BD;

						case 3:
							return this.HT;

						case 4:
							return this.LT;

						case 5:
							return this.FT;

						case 6:
							return this.CY;

						case 7:
							return this.HHO;

						case 8:
							return this.RD;

						case 9:
							return this.LC;

                        case 10:
                            return this.LP;

                        case 11:
                            return this.LBD;

					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch ( index )
					{
						case 0:
							this.HH = value;
							return;

						case 1:
							this.SD = value;
							return;

						case 2:
							this.BD = value;
							return;

						case 3:
							this.HT = value;
							return;

						case 4:
							this.LT = value;
							return;

						case 5:
							this.FT = value;
							return;

						case 6:
							this.CY = value;
							return;

						case 7:
							this.HHO = value;
							return;

						case 8:
							this.RD = value;
							return;

						case 9:
							this.LC = value;
							return;

                        case 10:
                            this.LP = value;
                            return;

                        case 11:
                            this.LBD = value;
                            return;

					}
					throw new IndexOutOfRangeException();
				}
			}
		}
        protected struct stmixer
        {
            internal bool bIsAdd;
            internal CSound csound;
        };

		public CAct演奏AVI actAVI;
		public CAct演奏BGA actBGA;

		protected CAct演奏チップファイアGB actChipFireGB;
		public CAct演奏Combo共通 actCombo;
		protected CAct演奏Danger共通 actDANGER;
		protected CActFIFOBlackStart actFI;
		protected CActFIFOBlack actFO;
		protected CActFIFOWhite actFOClear;
        public CActFIFOWhiteClear actFOStageClear;
        //protected CActStageClear actStageClear;
		public CAct演奏ゲージ共通 actGauge;
        public CAct演奏Drumsフィルインエフェクト actFillin;
		protected CAct演奏判定文字列共通 actJudgeString;
		protected CAct演奏DrumsレーンフラッシュD actLaneFlushD;
		protected CAct演奏レーンフラッシュGB共通 actLaneFlushGB;
		protected CAct演奏パネル文字列 actPanel;
		protected CAct演奏演奏情報 actPlayInfo;
		protected CAct演奏RGB共通 actRGB;
		public CAct演奏スコア共通 actScore;
		protected CAct演奏ステージ失敗 actStageFailed;
		public CAct演奏ステータスパネル共通 actStatusPanels;
		protected CAct演奏WailingBonus共通 actWailingBonus;
		public CAct演奏スクロール速度 act譜面スクロール速度;
		protected bool bPAUSE;
		protected STDGBVALUE<bool> b演奏にMIDI入力を使った;
		protected STDGBVALUE<bool> b演奏にキーボードを使った;
		protected STDGBVALUE<bool> b演奏にジョイパッドを使った;
		protected STDGBVALUE<bool> b演奏にマウスを使った;
		protected CCounter ctWailingチップ模様アニメ;
        public CCounter ctBPMバー;
        public CCounter ct登場用;
        public CCounter ctコンボ動作タイマ;

		protected STDGBVALUE<CCounter> ctチップ模様アニメ;
        protected abstract void tJudgeLineMovingUpandDown();
		protected E演奏画面の戻り値 eフェードアウト完了時の戻り値;
		protected readonly int[,] nBGAスコープチャンネルマップ = new int[ , ] { { 0xc4, 0xc7, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xe0 }, { 4, 7, 0x55, 0x56, 0x57, 0x58, 0x59, 0x60 } };
        protected readonly int[] nチャンネル0Atoパッド08 = new int[] { 1, 2, 3, 4, 5, 7, 6, 1, 8, 0, 9, 9 };
        protected readonly int[] nチャンネル0Atoレーン07 = new int[] { 1, 2, 3, 4, 5, 7, 6, 1, 9, 0, 8, 8 };
                                                                    //                         RD LC  LP  RD
		protected readonly int[] nパッド0Atoチャンネル0A = new int[] { 0x11, 0x12, 0x13, 0x14, 0x15, 0x17, 0x16, 0x18, 0x19, 0x1a, 0x1b, 0x1c };
        protected readonly int[] nパッド0Atoパッド08 = new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 8, 0, 9, 9 };// パッド画像のヒット処理用
                                                              //   HH SD BD HT LT FT CY HHO RD LC LP LBD
        protected readonly int[] nパッド0Atoレーン07 = new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 9, 0, 8, 8 };
        protected readonly float[,] fDamageGaugeDelta = new float[,] { { 0.004f, 0.006f, 0.006f }, { 0.002f, 0.003f, 0.003f }, { 0f, 0f, 0f }, { -0.02f, -0.03f, -0.03f }, { -0.05f, -0.05f, -0.05f } };
        protected readonly float[] fDamageLevelFactor = new float[] { 0.5f, 1f, 1.5f };

        public int nJudgeLinePosY = 561;//(CDTXMania.ConfigIni.bReverse.Drums ? 159 : 561);
        public int nShutterInPosY = 0;
        public int nShutterOutPosY = 0;
        public long n現在のスコア = 0;
		public STDGBVALUE<CHITCOUNTOFRANK> nヒット数・Auto含まない;
		public STDGBVALUE<CHITCOUNTOFRANK> nヒット数・Auto含む;
		protected int n現在のトップChip = -1;
		protected int[] n最後に再生したBGMの実WAV番号 = new int[ 50 ];
        protected static int nJudgeLineMaxPosY;
        protected static int nJudgeLineMinPosY;
        protected static int nShutterMaxPosY;
        protected static int nShutterMinPosY;
		protected int n最後に再生したHHのチャンネル番号;
		protected List<int> L最後に再生したHHの実WAV番号;		// #23921 2011.1.4 yyagi: change "int" to "List<int>", for recording multiple wav No.
		protected STLANEVALUE<int> n最後に再生した実WAV番号;	// #26388 2011.11.8 yyagi: change "n最後に再生した実WAV番号.GUITAR" and "n最後に再生した実WAV番号.BASS"
																//							into "n最後に再生した実WAV番号";
//		protected int n最後に再生した実WAV番号.GUITAR;
//		protected int n最後に再生した実WAV番号.BASS;

        protected volatile Queue<stmixer> queueMixerSound; // #24820 2013.1.21 yyagi まずは単純にAdd/Removeを1個のキューでまとめて管理するやり方で設計する
        protected DateTime dtLastQueueOperation; //
        protected bool bIsDirectSound; //
        protected double db再生速度;

        private CCounter[] ctタイマー = new CCounter[3];
        public bool bブーストボーナス = false;
	
		protected STDGBVALUE<Queue<CDTX.CChip>> queWailing;
		protected STDGBVALUE<CDTX.CChip> r現在の歓声Chip;
		protected CDTX.CChip r現在の空うちギターChip;
		protected STKARAUCHI r現在の空うちドラムChip;
		protected CDTX.CChip r現在の空うちベースChip;
		protected CDTX.CChip r次にくるギターChip;
		protected CDTX.CChip r次にくるベースChip;
		protected CTexture txWailing枠;
		protected CTexture txチップ;
		protected CTexture txヒットバー;

		protected CTexture tx背景;
		protected STDGBVALUE<int> nInputAdjustTimeMs;		// #23580 2011.1.3 yyagi
		public STAUTOPLAY bIsAutoPlay;		// #24239 2011.1.23 yyagi
//		protected int nRisky_InitialVar, nRiskyTime;		// #23559 2011.7.28 yyagi → CAct演奏ゲージ共通クラスに隠蔽
        protected int nPolyphonicSounds;

        protected List<CDTX.CChip> listChip;
	    protected Dictionary<int, CDTX.CWAV> listWAV;

		protected Stopwatch sw;		// 2011.6.13 最適化検討用のストップウォッチ
		protected Stopwatch sw2;
//		protected GCLatencyMode gclatencymode;

        public void AddMixer(CSound cs)
        {
            stmixer stm = new stmixer()
           {
               bIsAdd = true,
               csound = cs
           };
            queueMixerSound.Enqueue(stm);
            //Debug.WriteLine("★Queue: add " + Path.GetFileName(stm.csound.strファイル名));
        }
        public void RemoveMixer(CSound cs)
        {
            stmixer stm = new stmixer()
            {
                bIsAdd = false,
                csound = cs
            };
            queueMixerSound.Enqueue(stm);
            //Debug.WriteLine("★Queue: remove " + Path.GetFileName(stm.csound.strファイル名));
        }

		protected E判定 e指定時刻からChipのJUDGEを返す( long nTime, CDTX.CChip pChip, int nInputAdjustTime )
		{
			if ( pChip != null )
			{
				pChip.nLag = (int) ( nTime + nInputAdjustTime - pChip.n発声時刻ms );		// #23580 2011.1.3 yyagi: add "nInputAdjustTime" to add input timing adjust feature
				int nDeltaTime = Math.Abs( pChip.nLag );
				if ( nDeltaTime <= CDTXMania.nPerfect範囲ms )
				{
					return E判定.Perfect;
				}
				if ( nDeltaTime <= CDTXMania.nGreat範囲ms )
				{
					return E判定.Great;
				}
				if ( nDeltaTime <= CDTXMania.nGood範囲ms )
				{
					return E判定.Good;
				}
				if ( nDeltaTime <= CDTXMania.nPoor範囲ms )
				{
					return E判定.Poor;
				}
			}
			return E判定.Miss;
		}
		protected CDTX.CChip r空うちChip( E楽器パート part, Eパッド pad )
		{
			switch ( part )
			{
				case E楽器パート.DRUMS:
					switch ( pad )
					{
						case Eパッド.HH:
							if ( this.r現在の空うちドラムChip.HH != null )
							{
								return this.r現在の空うちドラムChip.HH;
							}
							if ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける )
							{
								if ( CDTXMania.ConfigIni.eHHGroup == EHHGroup.左シンバルのみ打ち分ける )
								{
									return this.r現在の空うちドラムChip.HHO;
								}
								if ( this.r現在の空うちドラムChip.HHO != null )
								{
									return this.r現在の空うちドラムChip.HHO;
								}
							}
							return this.r現在の空うちドラムChip.LC;

						case Eパッド.SD:
							return this.r現在の空うちドラムChip.SD;

						case Eパッド.BD:
							return this.r現在の空うちドラムChip.BD;

						case Eパッド.HT:
							return this.r現在の空うちドラムChip.HT;

						case Eパッド.LT:
							if ( this.r現在の空うちドラムChip.LT != null )
							{
								return this.r現在の空うちドラムChip.LT;
							}
							if ( CDTXMania.ConfigIni.eFTGroup == EFTGroup.共通 )
							{
								return this.r現在の空うちドラムChip.FT;
							}
							return null;

						case Eパッド.FT:
							if ( this.r現在の空うちドラムChip.FT != null )
							{
								return this.r現在の空うちドラムChip.FT;
							}
							if ( CDTXMania.ConfigIni.eFTGroup == EFTGroup.共通 )
							{
								return this.r現在の空うちドラムChip.LT;
							}
							return null;

						case Eパッド.CY:
							if ( this.r現在の空うちドラムChip.CY != null )
							{
								return this.r現在の空うちドラムChip.CY;
							}
							if ( CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通 )
							{
								return this.r現在の空うちドラムChip.RD;
							}
							return null;

						case Eパッド.HHO:
							if ( this.r現在の空うちドラムChip.HHO != null )
							{
								return this.r現在の空うちドラムChip.HHO;
							}
							if ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける )
							{
								if ( CDTXMania.ConfigIni.eHHGroup == EHHGroup.左シンバルのみ打ち分ける )
								{
									return this.r現在の空うちドラムChip.HH;
								}
								if ( this.r現在の空うちドラムChip.HH != null )
								{
									return this.r現在の空うちドラムChip.HH;
								}
							}
							return this.r現在の空うちドラムChip.LC;

						case Eパッド.RD:
							if ( this.r現在の空うちドラムChip.RD != null )
							{
								return this.r現在の空うちドラムChip.RD;
							}
							if ( CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通 )
							{
								return this.r現在の空うちドラムChip.CY;
							}
							return null;

						case Eパッド.LC:
							if ( this.r現在の空うちドラムChip.LC != null )
							{
								return this.r現在の空うちドラムChip.LC;
							}
							if ( ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける ) && ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.全部共通 ) )
							{
								return null;
							}
							if ( this.r現在の空うちドラムChip.HH != null )
							{
								return this.r現在の空うちドラムChip.HH;
							}
							return this.r現在の空うちドラムChip.HHO;

                        case Eパッド.LP:
                            if (this.r現在の空うちドラムChip.LP != null)
                            {
                                return this.r現在の空うちドラムChip.LP;
                            }
                            if (CDTXMania.ConfigIni.eBDGroup != EBDGroup.左右ペダルのみ打ち分ける)
                            {
                                if (this.r現在の空うちドラムChip.LBD != null)
                                {
                                    return this.r現在の空うちドラムChip.LBD;
                                }
                            }
                            return this.r現在の空うちドラムChip.BD;

                        case Eパッド.LBD:
                            if (this.r現在の空うちドラムChip.LBD != null)
                            {
                                return this.r現在の空うちドラムChip.LBD;
                            }
                            if (CDTXMania.ConfigIni.eBDGroup != EBDGroup.左右ペダルのみ打ち分ける)
                            {
                                if (this.r現在の空うちドラムChip.LP != null)
                                {
                                    return this.r現在の空うちドラムChip.LP;
                                }
                            }
                            return this.r現在の空うちドラムChip.BD;


					}
					break;

				case E楽器パート.GUITAR:
					return this.r現在の空うちギターChip;

				case E楽器パート.BASS:
					return this.r現在の空うちベースChip;
			}
			return null;
		}
		protected CDTX.CChip r指定時刻に一番近いChip・ヒット未済問わず不可視考慮( long nTime, int nChannel, int nInputAdjustTime )
		{
			sw2.Start();
			nTime += nInputAdjustTime;						// #24239 2011.1.23 yyagi InputAdjust

			int nIndex_InitialPositionSearchingToPast;
			if ( this.n現在のトップChip == -1 )				// 演奏データとして1個もチップがない場合は
			{
				sw2.Stop();
				return null;
			}
            int count = listChip.Count;
			int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = this.n現在のトップChip;
			if ( this.n現在のトップChip >= count )			// その時点で演奏すべきチップが既に全部無くなっていたら
			{
				nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = count - 1;
			}
			for ( ; nIndex_NearestChip_Future < count; nIndex_NearestChip_Future++)
			{
				CDTX.CChip chip = CDTXMania.DTX.listChip[ nIndex_NearestChip_Future ];
				if ( ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1c ) ) )
				{
					if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
					{
						if ( chip.n発声時刻ms > nTime )
						{
							break;
						}
						nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
					}
					continue;	// ほんの僅かながら高速化
				}
				else if ((nChannel == 0x2F && chip.e楽器パート == E楽器パート.GUITAR) || (((0x20 <= nChannel && nChannel <= 40) || (147 <= nChannel && nChannel <= 159) || (169 <= nChannel && nChannel <= 175) || (208 <= nChannel && nChannel <= 211)) && chip.nチャンネル番号 == nChannel))
				{
					if ( chip.n発声時刻ms > nTime )
					{
						break;
					}
					nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
				}
                else if ((nChannel == 175 && chip.e楽器パート == E楽器パート.BASS) || (((160 <= nChannel && nChannel <= 168) || (197 <= nChannel && nChannel <= 198) || (200 <= nChannel && nChannel <= 207) || (218 <= nChannel && nChannel <= 223) || (225 <= nChannel && nChannel <= 232)) && chip.nチャンネル番号 == nChannel))
				{
					if ( chip.n発声時刻ms > nTime )
					{
						break;
					}
					nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
				}
				// nIndex_NearestChip_Future++;
			}
			int nIndex_NearestChip_Past = nIndex_InitialPositionSearchingToPast;
			//while ( nIndex_NearestChip_Past >= 0 )			// 過去方向への検索
			for ( ; nIndex_NearestChip_Past >= 0; nIndex_NearestChip_Past-- )
			{
                CDTX.CChip chip = listChip[nIndex_NearestChip_Past];
				if ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1c ) )
				{
					if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
					{
						break;
					}
				}
				else if ((nChannel == 47 && chip.e楽器パート == E楽器パート.GUITAR) || (((0x20 <= nChannel && nChannel <= 0x28) || (147 <= nChannel && nChannel <= 159) || (169 <= nChannel && nChannel <= 175) || (208 <= nChannel && nChannel <= 211)) && chip.nチャンネル番号 == nChannel))
				{
					if ( ( 0x20 <= chip.nチャンネル番号 ) && ( chip.nチャンネル番号 <= 0x28 ) )
					{
						break;
					}
				}
				else if (((nChannel == 175 && chip.e楽器パート == E楽器パート.BASS) || (((160 <= nChannel && nChannel <= 168) || (197 <= nChannel && nChannel <= 198) || (200 <= nChannel && nChannel <= 207) || (218 <= nChannel && nChannel <= 223) || (225 <= nChannel && nChannel <= 232)) && chip.nチャンネル番号 == nChannel)) && ((160 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 168) || (197 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 198) || (200 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 207) || (218 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 223) || (225 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 232)))
				{
					break;
				}
				// nIndex_NearestChip_Past--;
			}

			if ( nIndex_NearestChip_Future >= count )
			{
				if ( nIndex_NearestChip_Past < 0 )	// 検索対象が過去未来どちらにも見つからなかった場合
				{
					return null;
				}
				else 								// 検索対象が未来方向には見つからなかった(しかし過去方向には見つかった)場合
				{
					sw2.Stop();
                    return listChip[nIndex_NearestChip_Past];
				}
			}
			else if ( nIndex_NearestChip_Past < 0 )	// 検索対象が過去方向には見つからなかった(しかし未来方向には見つかった)場合
			{
				sw2.Stop();
                return listChip[nIndex_NearestChip_Future];
			}
													// 検索対象が過去未来の双方に見つかったなら、より近い方を採用する
            CDTX.CChip nearestChip_Future = listChip[ nIndex_NearestChip_Future ];
	        CDTX.CChip nearestChip_Past = listChip[ nIndex_NearestChip_Past ];
			int nDiffTime_Future = Math.Abs( (int) ( nTime - nearestChip_Future.n発声時刻ms ) );
			int nDiffTime_Past   = Math.Abs( (int) ( nTime - nearestChip_Past.n発声時刻ms ) );
			if ( nDiffTime_Future >= nDiffTime_Past )
			{
				sw2.Stop();
				return nearestChip_Past;
			}
			sw2.Stop();
			return nearestChip_Future;
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, CDTXMania.ConfigIni.n手動再生音量, false, false );
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part, int n音量 )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, n音量, false, false );
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, n音量, bモニタ, false );
		}
        protected void tサウンド再生(CDTX.CChip pChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ, bool b音程をずらして再生)
        {
            if (pChip != null)
            {
                bool overwrite = false;
                switch (part)
                {
                    case E楽器パート.DRUMS:
                    #region [ DRUMS ]
                        {
                            int index = pChip.nチャンネル番号;
                            if ((0x11 <= index) && (index <= 0x1c))
                            {
                                index -= 0x11;
                            }
                            else if ((0x31 <= index) && (index <= 0x3c))
                            {
                                index -= 0x31;
                            }
                            // mute sound (auto)
                            // 4A: 84: HH (HO/HC)
                            // 4B: 85: CY
                            // 4C: 86: RD
                            // 4D: 87: LC
                            // 2A: 88: Gt
                            // AA: 89: Bs
                            else if (0x84 == index)	// 仮に今だけ追加 HHは消音処理があるので overwriteフラグ系の処理は改めて不要
                            {
                                index = 0;
                            }
                            else if ((0x85 <= index) && (index <= 0x87))	// 仮に今だけ追加
                            {
                                //            CY    RD    LC
                                int[] ch = { 0x16, 0x19, 0x1A };
                                pChip.nチャンネル番号 = ch[pChip.nチャンネル番号 - 0x85];
                                index = pChip.nチャンネル番号 - 0x11;
                                overwrite = true;
                            }
                            else
                            {
                                return;
                            }

                            int nLane = this.nチャンネル0Atoレーン07[index];
                            if (((((nLane == 1) && (index == 0)) && ((this.n最後に再生したHHのチャンネル番号 != 0x18) && (this.n最後に再生したHHのチャンネル番号 != 0x38))) || ((((nLane == 8)) && ((index == 10) && (this.n最後に再生したHHのチャンネル番号 != 0x18))) && (this.n最後に再生したHHのチャンネル番号 != 0x38))) && CDTXMania.ConfigIni.bMutingLP)
                            {
                                for (int i = 0; i < this.L最後に再生したHHの実WAV番号.Count; i++)
                                {
                                    CDTXMania.DTX.tWavの再生停止(this.L最後に再生したHHの実WAV番号[i]);
                                }
                                this.L最後に再生したHHの実WAV番号.Clear();
                                this.n最後に再生したHHのチャンネル番号 = pChip.nチャンネル番号;
                            }
                            if (nLane == 0 && CDTXMania.ConfigIni.bドラムセットを動かす)
                            {
                                this.actAVI.ct左シンバル.n現在の値 = 0;
                            }
                            if (nLane == 7 && CDTXMania.ConfigIni.bドラムセットを動かす)
                            {
                                this.actAVI.ct右シンバル.n現在の値 = 0;
                            }
                           switch (index)
                            {
                                case 0:
                                case 7:
                                case 0x20:
                                case 0x27:
                                    if (this.L最後に再生したHHの実WAV番号.Count >= 0x10)
                                    {
                                        this.L最後に再生したHHの実WAV番号.RemoveAt(0);
                                    }
                                    if (!this.L最後に再生したHHの実WAV番号.Contains(pChip.n整数値・内部番号))
                                    {
                                        this.L最後に再生したHHの実WAV番号.Add(pChip.n整数値・内部番号);
                                    }
                                    break;
                            }

                            CDTXMania.DTX.tチップの再生(pChip, n再生開始システム時刻ms, nLane, n音量, bモニタ);
                            this.n最後に再生した実WAV番号[nLane] = pChip.n整数値・内部番号;		// nLaneでなくindexにすると、LC(1A-11=09)とギター(enumで09)がかぶってLC音が消されるので注意
                            return;
                        }
                        #endregion
					case E楽器パート.GUITAR:
					#region [ GUITAR ]
						CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Guitar );
						CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, (int) Eレーン.Guitar, n音量, bモニタ, b音程をずらして再生 );
						this.n最後に再生した実WAV番号.Guitar = pChip.n整数値・内部番号;
						return;
					#endregion
					case E楽器パート.BASS:
					#region [ BASS ]
						CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Bass );
						CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, (int) Eレーン.Bass, n音量, bモニタ, b音程をずらして再生 );
						this.n最後に再生した実WAV番号.Bass = pChip.n整数値・内部番号;
						return;
					#endregion

					default:
						break;
				}
			}
		}
        protected void tステータスパネルの選択()
        {
            if (CDTXMania.bコンパクトモード)
            {
                this.actStatusPanels.tラベル名からステータスパネルを決定する(null);
            }
            else if (CDTXMania.stage選曲.r確定された曲 != null)
            {
                this.actStatusPanels.tラベル名からステータスパネルを決定する(CDTXMania.stage選曲.r確定された曲.ar難易度ラベル[CDTXMania.stage選曲.n確定された曲の難易度]);
            }
        }
		protected E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip )
		{
			return tチップのヒット処理( nHitTime, pChip, true );
		}
		protected abstract E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, bool bCorrectLane );
		protected E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, E楽器パート screenmode )		// E楽器パート screenmode
		{
			return tチップのヒット処理( nHitTime, pChip, screenmode, true );
		}
        protected E判定 tチップのヒット処理(long nHitTime, CDTX.CChip pChip, E楽器パート screenmode, bool bCorrectLane)
        {
            pChip.bHit = true;
            if (pChip.e楽器パート == E楽器パート.UNKNOWN)
            {
                this.bAUTOでないチップが１つでもバーを通過した = true;
            }
            bool bPChipIsAutoPlay = bCheckAutoPlay(pChip);
            pChip.bIsAutoPlayed = bPChipIsAutoPlay;			// 2011.6.10 yyagi
            E判定 eJudgeResult = E判定.Auto;
            switch (pChip.e楽器パート)
            {
                case E楽器パート.DRUMS:
                    {
                        int nInputAdjustTime = bPChipIsAutoPlay ? 0 : this.nInputAdjustTimeMs.Drums;
                        eJudgeResult = (bCorrectLane) ? this.e指定時刻からChipのJUDGEを返す(nHitTime, pChip, nInputAdjustTime) : E判定.Miss;
                        this.actJudgeString.Start(this.nチャンネル0Atoレーン07[pChip.nチャンネル番号 - 0x11], bPChipIsAutoPlay ? E判定.Auto : eJudgeResult, pChip.nLag);
                    }
                    break;

                case E楽器パート.GUITAR:
                    {
                        int nInputAdjustTime = bPChipIsAutoPlay ? 0 : this.nInputAdjustTimeMs.Guitar;
                        eJudgeResult = (bCorrectLane) ? this.e指定時刻からChipのJUDGEを返す(nHitTime, pChip, nInputAdjustTime) : E判定.Miss;
                        this.actJudgeString.Start(13, bPChipIsAutoPlay ? E判定.Auto : eJudgeResult, pChip.nLag);
                        break;
                    }

                case E楽器パート.BASS:
                    {
                        int nInputAdjustTime = bPChipIsAutoPlay ? 0 : this.nInputAdjustTimeMs.Bass;
                        eJudgeResult = (bCorrectLane) ? this.e指定時刻からChipのJUDGEを返す(nHitTime, pChip, nInputAdjustTime) : E判定.Miss;
                        this.actJudgeString.Start(14, bPChipIsAutoPlay ? E判定.Auto : eJudgeResult, pChip.nLag);
                    }
                    break;
            }

            if (CDTXMania.ConfigIni.bAutoAddGage == false)
            {
                if (!bPChipIsAutoPlay && (pChip.e楽器パート != E楽器パート.UNKNOWN))
                {
                    //this.t判定にあわせてゲージを増減する( screenmode, pChip.e楽器パート, eJudgeResult );
                    actGauge.Damage(screenmode, pChip.e楽器パート, eJudgeResult);
                }
            }
            else if (CDTXMania.ConfigIni.bAutoAddGage == true)
            {
                if ((pChip.e楽器パート != E楽器パート.UNKNOWN))
                {
                    actGauge.Damage(screenmode, pChip.e楽器パート, eJudgeResult);
                }
            }

            switch (pChip.e楽器パート)
            {
                case E楽器パート.DRUMS:
                    switch (eJudgeResult)
                    {
                        case E判定.Miss:
                        case E判定.Bad:
                            this.nヒット数・Auto含む.Drums.Miss++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない.Drums.Miss++;
                                this.actPlayInfo.nMISS数++;
                            }
                            break;
                        case E判定.Poor:
                            this.nヒット数・Auto含む.Drums.Poor++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない.Drums.Poor++;
                                this.actPlayInfo.nPOOR数++;
                            }
                            break;
                        case E判定.Good:
                            this.nヒット数・Auto含む.Drums.Good++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない.Drums.Good++;
                                this.actPlayInfo.nGOOD数++;
                            }
                            break;
                        case E判定.Great:
                            this.nヒット数・Auto含む.Drums.Great++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない.Drums.Great++;
                                this.actPlayInfo.nGREAT数++;
                            }
                            break;
                        case E判定.Perfect:
                            this.nヒット数・Auto含む.Drums.Perfect++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない.Drums.Perfect++;
                                this.actPlayInfo.nPERFECT数++;
                            }
                            break;
                        case E判定.Auto:
                            break;
                    }

                    if (CDTXMania.ConfigIni.bドラムが全部オートプレイである || !bPChipIsAutoPlay)
                    {
                        switch (eJudgeResult)
                        {
                            case E判定.Perfect:
                            case E判定.Great:
                            case E判定.Good:
                                this.actCombo.n現在のコンボ数.Drums++;
                                break;

                            default:
                                this.actCombo.n現在のコンボ数.Drums = 0;
                                break;
                        }
                    }
                    break;

                case E楽器パート.GUITAR:
                case E楽器パート.BASS:
                    int indexInst = (int)pChip.e楽器パート;
                    switch (eJudgeResult)
                    {
                        case E判定.Miss:
                        case E判定.Bad:
                            this.nヒット数・Auto含む[indexInst].Miss++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない[indexInst].Miss++;
                            }
                            break;
                        default:	// #24068 2011.1.10 ikanick changed
                            // #24167 2011.1.16 yyagi changed
                            this.nヒット数・Auto含む[indexInst][(int)eJudgeResult]++;
                            if (!bPChipIsAutoPlay)
                            {
                                this.nヒット数・Auto含まない[indexInst][(int)eJudgeResult]++;
                            }
                            break;
                    }
                    switch (eJudgeResult)
                    {
                        case E判定.Perfect:
                        case E判定.Great:
                        case E判定.Good:
                            this.actCombo.n現在のコンボ数[indexInst]++;
                            break;

                        default:
                            this.actCombo.n現在のコンボ数[indexInst] = 0;
                            break;
                    }
                    break;

                default:
                    break;
            }
            for (int i = 0; i < 3; i++)
            {
                if (this.ctタイマー[i] != null)
                {
                    if (!this.ctタイマー[i].b停止中)
                    {
                        if (this.ctタイマー[i].b終了値に達した)
                        {
                            this.bブーストボーナス = false;
                            this.ctタイマー[i].t停止();

                        }
                    }
                }
            }
            #region[スコア]
            //!bPChipIsAutoPlayを入れるとオート時にスコアを加算しなくなる。
            if (CDTXMania.ConfigIni.nSkillMode == 1)
            {
                int nRate = this.bブーストボーナス == true ? 2 : 1;
                if (CDTXMania.ConfigIni.bAutoAddGage == true)
                {
                    if (((pChip.e楽器パート != E楽器パート.UNKNOWN)) && (eJudgeResult != E判定.Miss) && (eJudgeResult != E判定.Bad))
                    {
                        int nCombos = this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート];
                        float nScoreDelta = 0;
                        float nComboMax;
                        nComboMax = CDTXMania.DTX.n可視チップ数.Drums;
                        if (eJudgeResult == E判定.Perfect)//ここでパフェ基準を作成。
                        {
                            if (nCombos < nComboMax)
                            {
                                nScoreDelta = (1000000.0f - 500.0f * CDTXMania.DTX.nボーナスチップ数 ) / 50.0f / (nComboMax - 24.5f);
                            }
                            //1000000÷50÷(その曲のMAXCOMBO-24.5)
                            else if (this.nヒット数・Auto含む.Drums.Perfect >= nComboMax)
                            {
                                nScoreDelta = 1000000.0f - (float)this.actScore.n現在の本当のスコア.Drums;
                            }
                            //1000000-PERFECT基準値×50×(その曲のMAXCOMBO-25.5)

                        }
                        else if (eJudgeResult == E判定.Great)
                        {
                            nScoreDelta = 1000000 / 50 / (nComboMax - 24.5f) * 0.5f;
                        }
                        else if (eJudgeResult == E判定.Good)
                        {
                            nScoreDelta = 1000000 / 50 / (nComboMax - 24.5f) * 0.2f;
                        }

                        if (nCombos < 50)
                        {
                            nScoreDelta = nScoreDelta * nCombos;
                        }
                        else if (nCombos == nComboMax || this.nヒット数・Auto含まない.Drums.Perfect == nComboMax)
                        {
                        }
                        else
                        {
                            nScoreDelta = nScoreDelta * 50;
                        }

                        this.actScore.Add(pChip.e楽器パート, bIsAutoPlay, (long)nScoreDelta);
                        this.actStatusPanels.nCurrentScore += (long)nScoreDelta;
                    }
                    //return eJudgeResult;
                }
                else
                {
                    if ((!bPChipIsAutoPlay && (pChip.e楽器パート != E楽器パート.UNKNOWN)) && (eJudgeResult != E判定.Miss) && (eJudgeResult != E判定.Bad))
                    {
                        int nCombos = this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート];
                        float nScoreDelta = 0;
                        float nComboMax;
                        nComboMax = CDTXMania.DTX.n可視チップ数.Drums;
                        if (eJudgeResult == E判定.Perfect)//ここでパフェ基準を作成。
                        {
                            if (nCombos < nComboMax)
                            {
                                nScoreDelta = (1000000.0f - 500.0f * CDTXMania.DTX.nボーナスチップ数) / 50.0f / (nComboMax - 24.5f);
                            }
                            //1000000÷50÷(その曲のMAXCOMBO-24.5)
                            else if (this.nヒット数・Auto含む.Drums.Perfect >= nComboMax)
                            {
                                nScoreDelta = 1000000.0f - (float)this.actScore.n現在の本当のスコア.Drums;
                            }
                            //1000000-PERFECT基準値×50×(その曲のMAXCOMBO-25.5)

                        }
                        else if (eJudgeResult == E判定.Great)
                        {
                            nScoreDelta = 1000000 / 50 / (nComboMax - 24.5f) * 0.5f;
                        }
                        else if (eJudgeResult == E判定.Good)
                        {
                            nScoreDelta = 1000000 / 50 / (nComboMax - 24.5f) * 0.2f;
                        }

                        if (nCombos < 50)
                        {
                            nScoreDelta = nScoreDelta * nCombos;
                        }
                        else if (nCombos == nComboMax || this.nヒット数・Auto含まない.Drums.Perfect == nComboMax)
                        {
                        }
                        else
                        {
                            nScoreDelta = nScoreDelta * 50;
                        }

                        this.actScore.Add(pChip.e楽器パート, bIsAutoPlay, (long)nScoreDelta * nRate);
                        this.actStatusPanels.nCurrentScore += (long)nScoreDelta * nRate;

                        //return eJudgeResult;
                    }
                }
            }
            else if (CDTXMania.ConfigIni.nSkillMode == 0)
            {
                if (CDTXMania.ConfigIni.bAutoAddGage)
                {
                    if (((pChip.e楽器パート != E楽器パート.UNKNOWN)) && (eJudgeResult != E判定.Miss) && (eJudgeResult != E判定.Bad))
                    {
                        int nCombos = this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート];
                        float nScoreDelta = 0;
                        long[] nComboScoreDelta = new long[] { 350L, 200L, 50L, 0L };
                        if ((nCombos <= 500) || (eJudgeResult == E判定.Good))
                        {
                            nScoreDelta = nComboScoreDelta[(int)eJudgeResult] * nCombos;
                        }
                        else if ((eJudgeResult == E判定.Perfect))
                        {
                            nScoreDelta = nComboScoreDelta[(int)eJudgeResult] * 500L;
                        }
                        this.actScore.Add(pChip.e楽器パート, bIsAutoPlay, (long)nScoreDelta);
                        this.actStatusPanels.nCurrentScore += (long)nScoreDelta;
                    }
                }
                else
                {
                    if ((!bPChipIsAutoPlay && (pChip.e楽器パート != E楽器パート.UNKNOWN)) && (eJudgeResult != E判定.Miss) && (eJudgeResult != E判定.Bad))
                    {
                        int nCombos = this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート];
                        float nScoreDelta = 0;
                        long[] nComboScoreDelta = new long[] { 350L, 200L, 50L, 0L };
                        if ((nCombos <= 500) || (eJudgeResult == E判定.Good))
                        {
                            nScoreDelta = nComboScoreDelta[(int)eJudgeResult] * nCombos;
                        }
                        else if ((eJudgeResult == E判定.Perfect))
                        {
                            nScoreDelta = nComboScoreDelta[(int)eJudgeResult] * 500L;
                        }
                        this.actScore.Add(pChip.e楽器パート, bIsAutoPlay, (long)nScoreDelta);
                        this.actStatusPanels.nCurrentScore += (long)nScoreDelta;
                    }
                }
            }
            #endregion
            #region[LivePoint]
            if (pChip.e楽器パート != E楽器パート.UNKNOWN)
            {
                for (int i = 0; i < 3; i++)
                {
                    STDGBVALUE<double> dbLPDelta = new STDGBVALUE<double>();
                    double[] db1打あたりのLP = new Double[3]; 
                    db1打あたりのLP[0] = 300.0 / (CDTXMania.DTX.n可視チップ数.Drums * 0.8);//ここでLPの基準値を作成。いわば1打あたりのLP値。
                    db1打あたりのLP[1] = 300.0 / (CDTXMania.DTX.n可視チップ数.Guitar * 0.8);//ここでLPの基準値を作成。いわば1打あたりのLP値。
                    db1打あたりのLP[2] = 300.0 / (CDTXMania.DTX.n可視チップ数.Bass * 0.8);//ここでLPの基準値を作成。いわば1打あたりのLP値。
                    if (eJudgeResult == E判定.Perfect)
                    {
                        dbLPDelta[i] = db1打あたりのLP[i] * 1.0;
                    }
                    else if (eJudgeResult == E判定.Great)
                    {
                        dbLPDelta[i] = db1打あたりのLP[i] * 1.0;
                    }
                    else if (eJudgeResult == E判定.Good)
                    {
                        dbLPDelta[i] = db1打あたりのLP[i] * 1.0;
                    }
                    else if (eJudgeResult == E判定.Poor)
                    {
                        if (this.actAVI.LivePoint[i] >= 1)
                        {
                            dbLPDelta[i] = -1.0;
                        }
                    }
                    else if (eJudgeResult == E判定.Miss)
                    {
                        if (this.actAVI.LivePoint[i] >= 2)
                            dbLPDelta[i] = -2.0;
                    }
                    else if (eJudgeResult == E判定.Auto)
                    {
                        dbLPDelta[i] = 0;
                    }

                    if (this.actAVI.LivePoint[i] <= 300)
                    {
                        this.actAVI.LivePoint[i] += dbLPDelta[i];
                    }
                }
            }
            #endregion

            return eJudgeResult;
        }
		protected abstract void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part );
		protected abstract void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part, int nLane );
		protected void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part, E楽器パート screenmode )
		{
			this.tチップのヒット処理・BadならびにTight時のMiss( part, 0, screenmode );
		}
		protected void tチップのヒット処理・BadならびにTight時のMiss( E楽器パート part, int nLane, E楽器パート screenmode )
		{
			this.bAUTOでないチップが１つでもバーを通過した = true;
			//this.t判定にあわせてゲージを増減する( screenmode, part, E判定.Miss );
			actGauge.Damage( screenmode, part, E判定.Miss );
			switch ( part )
			{
				case E楽器パート.DRUMS:
					if ( ( nLane >= 0 ) && ( nLane <= 10 ) )
					{
						this.actJudgeString.Start( nLane, bIsAutoPlay[ nLane ] ? E判定.Auto : E判定.Miss, 999 );
					}
					this.actCombo.n現在のコンボ数.Drums = 0;
					return;

				case E楽器パート.GUITAR:
					this.actJudgeString.Start( 13, E判定.Bad, 999 );
					this.actCombo.n現在のコンボ数.Guitar = 0;
					return;

				case E楽器パート.BASS:
					this.actJudgeString.Start( 14, E判定.Bad, 999 );
					this.actCombo.n現在のコンボ数.Bass = 0;
					break;

				default:
					return;
			}
		}
	
		protected CDTX.CChip r指定時刻に一番近い未ヒットChip( long nTime, int nChannelFlag, int nInputAdjustTime )
		{
			return this.r指定時刻に一番近い未ヒットChip( nTime, nChannelFlag, nInputAdjustTime, 0 );
		}
		protected CDTX.CChip r指定時刻に一番近い未ヒットChip( long nTime, int nChannel, int nInputAdjustTime, int n検索範囲時間ms )
		{
			sw2.Start();
//Trace.TraceInformation( "nTime={0}, nChannel={1:x2}, 現在のTop={2}", nTime, nChannel,CDTXMania.DTX.listChip[ this.n現在のトップChip ].n発声時刻ms );
			nTime += nInputAdjustTime;

			int nIndex_InitialPositionSearchingToPast;
			int nTimeDiff;
			if ( this.n現在のトップChip == -1 )			// 演奏データとして1個もチップがない場合は
			{
				sw2.Stop();
				return null;
			}
            int count = listChip.Count;
			int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = this.n現在のトップChip;
			if ( this.n現在のトップChip >= count )		// その時点で演奏すべきチップが既に全部無くなっていたら
			{
				nIndex_NearestChip_Future  = nIndex_InitialPositionSearchingToPast = count - 1;
			}
			// int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToFuture;
//			while ( nIndex_NearestChip_Future < count )	// 未来方向への検索
			for ( ; nIndex_NearestChip_Future < count; nIndex_NearestChip_Future++ )
			{
                CDTX.CChip chip = listChip[nIndex_NearestChip_Future];
				if ( !chip.bHit )
				{
					if ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1c ) )
					{
						if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
						{
							if ( chip.n発声時刻ms > nTime )
							{
								break;
							}
							nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
						}
						continue;
					}
                    else
                    {
                        if ((nChannel == 47 && chip.e楽器パート == E楽器パート.GUITAR) || (((32 <= nChannel && nChannel <= 40) || (147 <= nChannel && nChannel <= 159) || (169 <= nChannel && nChannel <= 175) || (208 <= nChannel && nChannel <= 211)) && chip.nチャンネル番号 == nChannel))
                        {
                            if ((long)chip.n発声時刻ms > nTime)
                            {
                                break;
                            }
                            nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
                        }
                        else
                        {
                            if ((nChannel == 175 && chip.e楽器パート == E楽器パート.BASS) || (((160 <= nChannel && nChannel <= 168) || (197 <= nChannel && nChannel <= 198) || (200 <= nChannel && nChannel <= 207) || (218 <= nChannel && nChannel <= 223) || (225 <= nChannel && nChannel <= 232)) && chip.nチャンネル番号 == nChannel))
                            {
                                if ((long)chip.n発声時刻ms > nTime)
                                {
                                    break;
                                }
                                nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
                            }
                        }
                    }
				}
//				nIndex_NearestChip_Future++;
			}
			int nIndex_NearestChip_Past = nIndex_InitialPositionSearchingToPast;
//			while ( nIndex_NearestChip_Past >= 0 )		// 過去方向への検索
			for ( ; nIndex_NearestChip_Past >= 0; nIndex_NearestChip_Past-- )
			{
                CDTX.CChip chip = listChip[nIndex_NearestChip_Past];
                if (!chip.bHit && ((nChannel >= 0x11 && nChannel <= 0x1c && (chip.nチャンネル番号 == nChannel || chip.nチャンネル番号 == nChannel + 32)) || (nChannel == 47 && chip.e楽器パート == E楽器パート.GUITAR) || (((32 <= nChannel && nChannel <= 40) || (147 <= nChannel && nChannel <= 159) || (169 <= nChannel && nChannel <= 175) || (208 <= nChannel && nChannel <= 211)) && chip.nチャンネル番号 == nChannel) || (nChannel == 175 && chip.e楽器パート == E楽器パート.BASS) || (((160 <= nChannel && nChannel <= 168) || (197 <= nChannel && nChannel <= 198) || (200 <= nChannel && nChannel <= 207) || (218 <= nChannel && nChannel <= 223) || (225 <= nChannel && nChannel <= 232)) && chip.nチャンネル番号 == nChannel)))
                {
                    break;
                }
//				nIndex_NearestChip_Past--;
			}
			if ( ( nIndex_NearestChip_Future >= count ) && ( nIndex_NearestChip_Past < 0 ) )	// 検索対象が過去未来どちらにも見つからなかった場合
			{
				sw2.Stop();
				return null;
			}
			CDTX.CChip nearestChip;	// = null;	// 以下のifブロックのいずれかで必ずnearestChipには非nullが代入されるので、null初期化を削除
			if ( nIndex_NearestChip_Future >= count )											// 検索対象が未来方向には見つからなかった(しかし過去方向には見つかった)場合
			{
                nearestChip = listChip[nIndex_NearestChip_Past];
//				nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			}
			else if ( nIndex_NearestChip_Past < 0 )												// 検索対象が過去方向には見つからなかった(しかし未来方向には見つかった)場合
			{
                nearestChip = listChip[nIndex_NearestChip_Future];
//				nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			}
			else
			{
				int nTimeDiff_Future = Math.Abs( (int) ( nTime - listChip[ nIndex_NearestChip_Future ].n発声時刻ms ) );
	            int nTimeDiff_Past = Math.Abs( (int) ( nTime - listChip[ nIndex_NearestChip_Past ].n発声時刻ms ) );
				if ( nTimeDiff_Future < nTimeDiff_Past )
				{
                    nearestChip = listChip [nIndex_NearestChip_Future];
//					nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
				}
				else
				{
					nearestChip = listChip[ nIndex_NearestChip_Past ];
//					nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
				}
			}
			nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			if ( ( n検索範囲時間ms > 0 ) && ( nTimeDiff > n検索範囲時間ms ) )					// チップは見つかったが、検索範囲時間外だった場合
			{
				sw2.Stop();
				return null;
			}
			sw2.Stop();
			return nearestChip;
		}

		protected CDTX.CChip r次に来る指定楽器Chipを更新して返す( E楽器パート inst )
		{
			switch ( (int) inst )
			{
				case (int)E楽器パート.GUITAR:
					return r次にくるギターChipを更新して返す();
				case (int)E楽器パート.BASS:
					return r次にくるベースChipを更新して返す();
				default:
					return null;
			}
		}
		protected CDTX.CChip r次にくるギターChipを更新して返す()
		{
			int nInputAdjustTime = this.bIsAutoPlay.GtPick ? 0 : this.nInputAdjustTimeMs.Guitar;
            this.r次にくるギターChip = this.r指定時刻に一番近い未ヒットChip(CSound管理.rc演奏用タイマ.n現在時刻, 47, nInputAdjustTime, 500);
			return this.r次にくるギターChip;
		}
		protected CDTX.CChip r次にくるベースChipを更新して返す()
		{
			int nInputAdjustTime = this.bIsAutoPlay.BsPick ? 0 : this.nInputAdjustTimeMs.Bass;
            this.r次にくるベースChip = this.r指定時刻に一番近い未ヒットChip(CSound管理.rc演奏用タイマ.n現在時刻, 0xaf, nInputAdjustTime, 500);
			return this.r次にくるベースChip;
		}

		protected void ChangeInputAdjustTimeInPlaying( IInputDevice keyboard, int plusminus )		// #23580 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
		{
			int part, offset = plusminus;
			if ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) )	// Guitar InputAdjustTime
			{
				part = (int) E楽器パート.GUITAR;
			}
			else if ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftAlt ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightAlt ) )	// Bass InputAdjustTime
			{
				part = (int) E楽器パート.BASS;
			}
			else	// Drums InputAdjustTime
			{
				part = (int) E楽器パート.DRUMS;
			}
			if ( !keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftControl ) && !keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightControl ) )
			{
				offset *= 10;
			}

			this.nInputAdjustTimeMs[ part ] += offset;
			if ( this.nInputAdjustTimeMs[ part ] > 99 )
			{
				this.nInputAdjustTimeMs[ part ] = 99;
			}
			else if ( this.nInputAdjustTimeMs[ part ] < -99 )
			{
				this.nInputAdjustTimeMs[ part ] = -99;
			}
			CDTXMania.ConfigIni.nInputAdjustTimeMs[ part ] = this.nInputAdjustTimeMs[ part ];
		}

		protected abstract void t入力処理・ドラム();
		protected abstract void ドラムスクロール速度アップ();
		protected abstract void ドラムスクロール速度ダウン();
        protected void tキー入力()
        {
            IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
            if (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.F1))
            {	// shift+f1 (pause)
                this.bPAUSE = !this.bPAUSE;
                if (this.bPAUSE)
                {
                    CSound管理.rc演奏用タイマ.t一時停止();
                    CDTXMania.Timer.t一時停止();
                    CDTXMania.DTX.t全チップの再生一時停止();
                }
                else
                {
                    CSound管理.rc演奏用タイマ.t再開();
                    CDTXMania.Timer.t再開();
                    CDTXMania.DTX.t全チップの再生再開();
                }
            }
            if (((base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED)) && (base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト))
            {
                if (!this.bPAUSE)
                {
                    this.t入力処理・ドラム();
                    this.t入力処理・ギターベース(E楽器パート.GUITAR);
                    this.t入力処理・ギターベース(E楽器パート.BASS);
                }
                if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.UpArrow) && (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightShift) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftShift)))
                {	// shift (+ctrl) + UpArrow (BGMAdjust)
                    CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する((keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftControl) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightControl)) ? 1 : 10);
                    CDTXMania.DTX.tWave再生位置自動補正();
                }
                else if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.DownArrow) && (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightShift) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftShift)))
                {	// shift + DownArrow (BGMAdjust)
                    CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する((keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftControl) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightControl)) ? -1 : -10);
                    CDTXMania.DTX.tWave再生位置自動補正();
                }
                else if (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.UpArrow))
                {	// UpArrow(scrollspeed up)
                    ドラムスクロール速度アップ();
                }
                else if (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.DownArrow))
                {	// DownArrow (scrollspeed down)
                    ドラムスクロール速度ダウン();
                }

                else if (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Delete))
                {	// del (debug info)
                    CDTXMania.ConfigIni.b演奏情報を表示する = !CDTXMania.ConfigIni.b演奏情報を表示する;
                }
                else if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.LeftArrow))		// #24243 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
                {
                    ChangeInputAdjustTimeInPlaying(keyboard, -1);
                }
                else if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.RightArrow))		// #24243 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
                {
                    ChangeInputAdjustTimeInPlaying(keyboard, +1);
                }
                else if (!this.bPAUSE && (base.eフェーズID == CStage.Eフェーズ.共通_通常状態) && (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.Escape) || CDTXMania.Pad.b押されたGB(Eパッド.FT)))
                {	// escape (exit)
                    this.actFO.tフェードアウト開始();
                    base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
                    this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.演奏中断;
                }

                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.PageUp))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nJudgeLinePosY > nJudgeLineMinPosY)
                        {
                            this.nJudgeLinePosY--;
                        }
                    }
                    else
                    {
                        if (this.sw.ElapsedMilliseconds > 10L)
                        {
                            if (this.sw2.IsRunning)
                            {
                                if (this.sw2.ElapsedMilliseconds > 100L)
                                {
                                    this.sw2.Reset();
                                }
                            }
                            else if (this.nJudgeLinePosY > nJudgeLineMinPosY)
                            {
                                this.nJudgeLinePosY--;
                            }
                            this.sw.Reset();
                            this.sw.Start();
                        }
                    }
                    CDTXMania.ConfigIni.nJudgeLine = nJudgeLineMaxPosY - this.nJudgeLinePosY;
                    CDTXMania.stage演奏ドラム画面.tJudgeLineMovingUpandDown();
                }
                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.PageDown))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nJudgeLinePosY < nJudgeLineMaxPosY)
                        {
                            this.nJudgeLinePosY++;
                        }
                    }
                    else if (this.sw.ElapsedMilliseconds > 10L)
                    {
                        if (this.sw2.IsRunning)
                        {
                            if (this.sw2.ElapsedMilliseconds > 100L)
                            {
                                this.sw2.Reset();
                            }
                        }
                        else if (this.nJudgeLinePosY < nJudgeLineMaxPosY)
                        {
                            this.nJudgeLinePosY++;
                        }
                        this.sw.Reset();
                        this.sw.Start();
                    }
                    CDTXMania.ConfigIni.nJudgeLine = nJudgeLineMaxPosY - this.nJudgeLinePosY;
                    CDTXMania.stage演奏ドラム画面.tJudgeLineMovingUpandDown();
                }

                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.NumberPad8))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nShutterInPosY > 0)
                        {
                            this.nShutterInPosY--;
                        }
                    }
                    else if (this.sw.ElapsedMilliseconds > 10L)
                    {
                        if (this.sw2.IsRunning)
                        {
                            if (this.sw2.ElapsedMilliseconds > 100L)
                            {
                                this.sw2.Reset();
                            }
                        }
                        else if (this.nShutterInPosY > 0)
                        {
                            this.nShutterInPosY--;
                        }
                        this.sw.Reset();
                        this.sw.Start();
                    }
                    CDTXMania.ConfigIni.nShutterInSide = this.nShutterInPosY;
                }
                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.NumberPad2))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nShutterInPosY < 100)
                        {
                            if(this.nShutterInPosY + this.nShutterOutPosY <= 99)
                            this.nShutterInPosY++;
                        }
                    }
                    else if (this.sw.ElapsedMilliseconds > 10L)
                    {
                        if (this.sw2.IsRunning)
                        {
                            if (this.sw2.ElapsedMilliseconds > 100L)
                            {
                                this.sw2.Reset();
                            }
                        }
                        else if (this.nShutterInPosY < 100)
                        {
                            if (this.nShutterInPosY + this.nShutterOutPosY <= 99)
                            this.nShutterInPosY++;
                        }
                        this.sw.Reset();
                        this.sw.Start();
                    }
                    CDTXMania.ConfigIni.nShutterInSide = this.nShutterInPosY;
                }
                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.NumberPad4))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nShutterOutPosY < 100)
                        {
                            if (this.nShutterInPosY + this.nShutterOutPosY <= 99)
                            this.nShutterOutPosY++;
                        }
                    }
                    else if (this.sw.ElapsedMilliseconds > 10L)
                    {
                        if (this.sw2.IsRunning)
                        {
                            if (this.sw2.ElapsedMilliseconds > 100L)
                            {
                                this.sw2.Reset();
                            }
                        }
                        else if (this.nShutterOutPosY < 100)
                        {
                            if (this.nShutterInPosY + this.nShutterOutPosY <= 99)
                            this.nShutterOutPosY++;
                        }
                        this.sw.Reset();
                        this.sw.Start();
                    }
                    CDTXMania.ConfigIni.nShutterOutSide = this.nShutterOutPosY;
                }
                if (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.NumberPad6))
                {
                    if (!this.sw.IsRunning)
                    {
                        this.sw2 = Stopwatch.StartNew();
                        this.sw = Stopwatch.StartNew();
                        if (this.nShutterOutPosY > 0)
                        {
                            this.nShutterOutPosY--;
                        }
                    }
                    else if (this.sw.ElapsedMilliseconds > 10L)
                    {
                        if (this.sw2.IsRunning)
                        {
                            if (this.sw2.ElapsedMilliseconds > 100L)
                            {
                                this.sw2.Reset();
                            }
                        }
                        else if (this.nShutterOutPosY > 0)
                        {
                            this.nShutterOutPosY--;
                        }
                        this.sw.Reset();
                        this.sw.Start();
                    }
                    CDTXMania.ConfigIni.nShutterOutSide = this.nShutterOutPosY;
                }

                if (keyboard.bキーが押された(0x3a))
                {
                    CConfigIni configIni = CDTXMania.ConfigIni;
                    configIni.nMovieMode++;
                    if (CDTXMania.ConfigIni.nMovieMode >= 4)
                    {
                        CDTXMania.ConfigIni.nMovieMode = 0;
                    }
                    this.actAVI.MovieMode();
                }
                if (keyboard.bキーが押された(0x3b))
                {
                    CConfigIni configIni = CDTXMania.ConfigIni;
                    configIni.nInfoType++;
                    if (CDTXMania.ConfigIni.nInfoType >= 2)
                    {
                        CDTXMania.ConfigIni.nInfoType = 0;
                    }
                }
                if ((keyboard.bキーが押されている(0x3c)))
                {
                    //SHIFT & F7
                    //CDTXMania.stage演奏ドラム画面.actGauge.db現在のゲージ値.Drums = 1.0;
                    //CDTXMania.stage演奏ドラム画面.actAVI.LivePoint = 300.0;
                    //CDTXMania.stage演奏ドラム画面.actGraph.dbグラフ値現在_渡 = 100.0;
                }
                if ( keyboard.bキーが押された(0x3d))
                {
                    //F8キー

                }
            }
        }

		protected void t入力メソッド記憶( E楽器パート part )
		{
			if ( CDTXMania.Pad.st検知したデバイス.Keyboard )
			{
				this.b演奏にキーボードを使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.Joypad )
			{
				this.b演奏にジョイパッドを使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.MIDIIN )
			{
				this.b演奏にMIDI入力を使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.Mouse )
			{
				this.b演奏にマウスを使った[ (int) part ] = true;
			}
		}


		protected abstract void t進行描画・AVI();
		protected void t進行描画・AVI(int x, int y)
		{
            if (((base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED) && (base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト)) && (!CDTXMania.ConfigIni.bストイックモード))
			{
				this.actAVI.t進行描画( x, y );
			}
		}
		protected abstract void t進行描画・BGA();
		protected void t進行描画・BGA(int x, int y)
		{
			if ( ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) && ( CDTXMania.ConfigIni.bBGA有効 ) )
			{
				this.actBGA.t進行描画( x, y );
			}
		}
		protected abstract void t進行描画・DANGER();
		protected void t進行描画・MIDIBGM()
		{
			if ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED )
			{
				CStage.Eフェーズ eフェーズid1 = base.eフェーズID;
			}
		}
		protected void t進行描画・RGBボタン()
		{
			if ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL )
			{
				this.actRGB.On進行描画();
			}
		}
		protected void t進行描画・STAGEFAILED()
		{
			if ( ( ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED ) || ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) && ( ( this.actStageFailed.On進行描画() != 0 ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) )
			{
				this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージ失敗;
				base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト;
				this.actFO.tフェードアウト開始();
			}
		}
		protected void t進行描画・WailingBonus()
		{
			if ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actWailingBonus.On進行描画();
			}
		}
		protected abstract void t進行描画・Wailing枠();
		protected void t進行描画・Wailing枠(int GtWailingFrameX, int BsWailingFrameX, int GtWailingFrameY, int BsWailingFrameY)
		{
			if ( ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				if ( this.txWailing枠 != null )
				{
					if ( CDTXMania.DTX.bチップがある.Guitar )
					{
						this.txWailing枠.t2D描画( CDTXMania.app.Device, GtWailingFrameX, GtWailingFrameY );
					}
					if ( CDTXMania.DTX.bチップがある.Bass )
					{
						this.txWailing枠.t2D描画( CDTXMania.app.Device, BsWailingFrameX, BsWailingFrameY );
					}
				}
			}
		}


		protected void t進行描画・チップファイアGB()
		{
			this.actChipFireGB.On進行描画();
		}
		protected abstract void t進行描画・パネル文字列();
		protected void t進行描画・パネル文字列(int x, int y)
		{
			if ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actPanel.t進行描画( x, y );
			}
		}
		protected void tパネル文字列の設定()
		{
			this.actPanel.SetPanelString( string.IsNullOrEmpty( CDTXMania.DTX.PANEL ) ? CDTXMania.stage選曲.r確定された曲.strタイトル : CDTXMania.DTX.PANEL );
		}

		protected void t進行描画・ゲージ()
		{
				this.actGauge.On進行描画();
		}
		protected void t進行描画・コンボ()
		{
			this.actCombo.On進行描画();
		}
		protected void t進行描画・スコア()
		{
			this.actScore.On進行描画();
		}
		protected void t進行描画・ステータスパネル()
		{
			this.actStatusPanels.On進行描画();
		}
		protected bool t進行描画・チップ( E楽器パート ePlayMode )
		{
			if ( ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED ) || ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				return true;
			}
            if ((this.n現在のトップChip == -1) || (this.n現在のトップChip >= listChip.Count))
			{
				return true;
			}
			if ( this.n現在のトップChip == -1 )
			{
				return true;
			}

            const double speed = 286;	// BPM150の時の1小節の長さ[dot]
            //XGのHS4.5が1289。思えばBPMじゃなくて拍の長さが関係あるよね。

			double ScrollSpeedDrums = ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1.0 ) * 0.5 * 37.5 * speed / 60000.0;
			double ScrollSpeedGuitar = ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Guitar + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;
			double ScrollSpeedBass = ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Bass + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;

			CDTX dTX = CDTXMania.DTX;
			CConfigIni configIni = CDTXMania.ConfigIni;
			for ( int nCurrentTopChip = this.n現在のトップChip; nCurrentTopChip < dTX.listChip.Count; nCurrentTopChip++ )
			{
				CDTX.CChip pChip = dTX.listChip[ nCurrentTopChip ];
                //Debug.WriteLine( "nCurrentTopChip=" + nCurrentTopChip + ", ch=" + pChip.nチャンネル番号.ToString("x2") + ", 発音位置=" + pChip.n発声位置 + ", 発声時刻ms=" + pChip.n発声時刻ms );
				pChip.nバーからの距離dot.Drums = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedDrums );
                pChip.nバーからの距離dot.Guitar = (int)((pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻) * ScrollSpeedGuitar);
                pChip.nバーからの距離dot.Bass = (int)((pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻) * ScrollSpeedBass);
				if ( Math.Min( Math.Min( pChip.nバーからの距離dot.Drums, pChip.nバーからの距離dot.Guitar ), pChip.nバーからの距離dot.Bass ) > 600 )
				{
					break;
				}
//				if ( ( ( nCurrentTopChip == this.n現在のトップChip ) && ( pChip.nバーからの距離dot.Drums < -65 ) ) && pChip.bHit )
				// #28026 2012.4.5 yyagi; 信心ワールドエンドの曲終了後リザルトになかなか行かない問題の修正
				if ( ( dTX.listChip[ this.n現在のトップChip ].nバーからの距離dot.Drums < -65 ) && dTX.listChip[ this.n現在のトップChip ].bHit )
				{
//					nCurrentTopChip = ++this.n現在のトップChip;
					++this.n現在のトップChip;
					continue;
				}

				bool bPChipIsAutoPlay = bCheckAutoPlay( pChip );

				int nInputAdjustTime = ( bPChipIsAutoPlay || (pChip.e楽器パート == E楽器パート.UNKNOWN) )? 0 : this.nInputAdjustTimeMs[ (int) pChip.e楽器パート ];

				if ( ( ( pChip.e楽器パート != E楽器パート.UNKNOWN ) && !pChip.bHit ) &&
                    ((pChip.nバーからの距離dot.Drums < 0) && (this.e指定時刻からChipのJUDGEを返す( CSound管理.rc演奏用タイマ.n現在時刻, pChip, nInputAdjustTime) == E判定.Miss)))
				{
                    this.tチップのヒット処理( CSound管理.rc演奏用タイマ.n現在時刻, pChip);
				}
				switch ( pChip.nチャンネル番号 )
				{
                    //描画順の都合上こちらから描画。

					#region [ 11-1c: ドラム演奏 ]
					case 0x11:	// ドラム演奏
					case 0x12:
					case 0x13:
					case 0x14:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
                    case 0x1b:
                    case 0x1c:
						this.t進行描画・チップ・ドラムス( configIni, ref dTX, ref pChip );
						break;
					#endregion

                    #region [ 01: BGM ]
                    case 0x01:	// BGM
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                            if (configIni.bBGM音を発声する)
                            {
                                dTX.tチップの再生(pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, (int)Eレーン.BGM, dTX.nモニタを考慮した音量(E楽器パート.UNKNOWN));
                            }
                            if (CDTXMania.DTX.bチップがある.AVI == false)
                            {
                                if (configIni.bAVI有効)
                                {
                                    this.actAVI.Start(0x54, pChip.rAVI, pChip.rDShow, 1280, 720, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, pChip.n発声時刻ms);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region [ 03: BPM変更 ]
                    case 0x03:	// BPM変更
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                            this.actPlayInfo.dbBPM = (pChip.n整数値 * (((double)configIni.n演奏速度) / 20.0)) + dTX.BASEBPM;
                            CDTXMania.stage演奏ドラム画面.UnitTime = (int)((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 13.2 * 1000.0));
                            CDTXMania.stage演奏ドラム画面.ctBPMバー = new CCounter(1, 14, CDTXMania.stage演奏ドラム画面.UnitTime, CDTXMania.Timer);
                            CDTXMania.stage演奏ドラム画面.ctコンボ動作タイマ = new CCounter(1, 16, (int)((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0 * 1000.0)), CDTXMania.Timer);
                        }
                        break;
                    #endregion
                    #region [ 04, 07, 55, 56,57, 58, 59, 60:レイヤーBGA ]
                    case 0x04:	// レイヤーBGA
                    case 0x07:
                    case 0x55:
                    case 0x56:
                    case 0x57:
                    case 0x58:
                    case 0x59:
                    case 0x60:
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                            if (configIni.bBGA有効)
                            {
                                switch (pChip.eBGA種別)
                                {
                                    case EBGA種別.BMPTEX:
                                        if (pChip.rBMPTEX != null)
                                        {
                                            this.actBGA.Start(pChip.nチャンネル番号, null, pChip.rBMPTEX, pChip.rBMPTEX.tx画像.sz画像サイズ.Width, pChip.rBMPTEX.tx画像.sz画像サイズ.Height, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                                        }
                                        break;

                                    case EBGA種別.BGA:
                                        if ((pChip.rBGA != null) && ((pChip.rBMP != null) || (pChip.rBMPTEX != null)))
                                        {
                                            this.actBGA.Start(pChip.nチャンネル番号, pChip.rBMP, pChip.rBMPTEX, pChip.rBGA.pt画像側右下座標.X - pChip.rBGA.pt画像側左上座標.X, pChip.rBGA.pt画像側右下座標.Y - pChip.rBGA.pt画像側左上座標.Y, 0, 0, pChip.rBGA.pt画像側左上座標.X, pChip.rBGA.pt画像側左上座標.Y, 0, 0, pChip.rBGA.pt表示座標.X, pChip.rBGA.pt表示座標.Y, 0, 0, 0);
                                        }
                                        break;

                                    case EBGA種別.BGAPAN:
                                        if ((pChip.rBGAPan != null) && ((pChip.rBMP != null) || (pChip.rBMPTEX != null)))
                                        {
                                            this.actBGA.Start(pChip.nチャンネル番号, pChip.rBMP, pChip.rBMPTEX, pChip.rBGAPan.sz開始サイズ.Width, pChip.rBGAPan.sz開始サイズ.Height, pChip.rBGAPan.sz終了サイズ.Width, pChip.rBGAPan.sz終了サイズ.Height, pChip.rBGAPan.pt画像側開始位置.X, pChip.rBGAPan.pt画像側開始位置.Y, pChip.rBGAPan.pt画像側終了位置.X, pChip.rBGAPan.pt画像側終了位置.Y, pChip.rBGAPan.pt表示側開始位置.X, pChip.rBGAPan.pt表示側開始位置.Y, pChip.rBGAPan.pt表示側終了位置.X, pChip.rBGAPan.pt表示側終了位置.Y, pChip.n総移動時間);
                                        }
                                        break;

                                    default:
                                        if (pChip.rBMP != null)
                                        {
                                            this.actBGA.Start(pChip.nチャンネル番号, pChip.rBMP, null, pChip.rBMP.n幅, pChip.rBMP.n高さ, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region [ 08: BPM変更(拡張) ]
                    case 0x08:	// BPM変更(拡張)
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                            if (dTX.listBPM.ContainsKey(pChip.n整数値・内部番号))
                            {
                                this.actPlayInfo.dbBPM = (dTX.listBPM[pChip.n整数値・内部番号].dbBPM値 * (((double)configIni.n演奏速度) / 20.0)) + dTX.BASEBPM;
                                CDTXMania.stage演奏ドラム画面.UnitTime = (int)((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 13.2 * 1000.0));
                                CDTXMania.stage演奏ドラム画面.ctBPMバー = new CCounter(1, 14, CDTXMania.stage演奏ドラム画面.UnitTime, CDTXMania.Timer);
                                CDTXMania.stage演奏ドラム画面.ctコンボ動作タイマ = new CCounter(1, 16, (int)((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0 * 1000.0)), CDTXMania.Timer);
                            }
                        }
                        break;
                    #endregion

					#region [ 1f: フィルインサウンド(ドラム) ]
					case 0x1f:	// フィルインサウンド(ドラム)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Drums = pChip;
						}
						break;
					#endregion
					#region [ 20-27: ギター演奏 ]
					case 0x20:	// ギター演奏
					case 0x21:
					case 0x22:
					case 0x23:
					case 0x24:
					case 0x25:
					case 0x26:
					case 0x27:

                        
                    case 0x93:
                    case 0x94:
                    case 0x95:
                    case 0x96:
                    case 0x97:
                    case 0x98:
                    case 0x99:
                    case 0x9A:
                    case 0x9B:
                    case 0x9C:
                    case 0x9D:
                    case 0x9E:
                    case 0x9F:
                    case 0xA9:
                    case 0xAA:
                    case 0xAB:
                    case 0xAC:
                    case 0xAD:
                    case 0xAE:
                    case 0xAF:
                    case 0xD0:
                    case 0xD1:
                    case 0xD2:
                    case 0xD3:
                         
						this.t進行描画・チップ・ギターベース( configIni, ref dTX, ref pChip, E楽器パート.GUITAR );
						break;
					#endregion
					#region [ 28: ウェイリング(ギター) ]
					case 0x28:	// ウェイリング(ギター)
						this.t進行描画・チップ・ギター・ウェイリング( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ 2f: ウェイリングサウンド(ギター) ]
					case 0x2f:	// ウェイリングサウンド(ギター)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Guitar = pChip;
						}
						break;
					#endregion
					#region [ 31-3a: 不可視チップ配置(ドラム) ]
					case 0x31:	// 不可視チップ配置(ドラム)
					case 0x32:
					case 0x33:
					case 0x34:
					case 0x35:
					case 0x36:
					case 0x37:
					case 0x38:
					case 0x39:
					case 0x3a:
                    case 0x3b:
                    case 0x3c:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion
                    #region [ 4F: ボーナス ]
					case 0x4F:	// フィルイン
						this.t進行描画・チップ・ボーナス( configIni, ref dTX, ref pChip );
						break;
					#endregion
                    #region [ 50: 小節線 ]
                    case 0x50:	// 小節線
                        {
                            this.t進行描画・チップ・小節線(configIni, ref dTX, ref pChip);
                            break;
                        }
                    #endregion
                    #region [ 51: 拍線 ]
                    case 0x51:	// 拍線
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                        }
                        if ((ePlayMode == E楽器パート.DRUMS) && (configIni.nLaneDisp.Drums == 0 || configIni.nLaneDisp.Drums == 1) && pChip.b可視 && (this.txチップ != null))
                        {
                            this.txチップ.t2D描画(CDTXMania.app.Device, 0x127, configIni.bReverse.Drums ? ((0x38 + pChip.nバーからの距離dot.Drums) - 1) : ((this.nJudgeLinePosY - pChip.nバーからの距離dot.Drums) - 1), new Rectangle(0, 772, 0x22f, 2));
                        }
                        break;
                    #endregion

					#region [ 52: MIDIコーラス ]
					case 0x52:	// MIDIコーラス
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion
					#region [ 53: フィルイン ]
					case 0x53:	// フィルイン
						this.t進行描画・チップ・フィルイン( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ 54: 動画再生 ]
					case 0x54:	// 動画再生
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            pChip.bHit = true;
                            if (configIni.bAVI有効)
                            {
                                switch (pChip.eAVI種別)
                                {
                                    case EAVI種別.AVI:
                                        if (pChip.rAVI != null)
                                        {
                                            this.actAVI.Start(pChip.nチャンネル番号, pChip.rAVI, pChip.rDShow, 278, 355, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, pChip.n発声時刻ms);
                                        }
                                        break;

                                    case EAVI種別.AVIPAN:
                                        if (pChip.rAVIPan != null)
                                        {
                                            this.actAVI.Start(pChip.nチャンネル番号, pChip.rAVI, pChip.rDShow, pChip.rAVIPan.sz開始サイズ.Width, pChip.rAVIPan.sz開始サイズ.Height, pChip.rAVIPan.sz終了サイズ.Width, pChip.rAVIPan.sz終了サイズ.Height, pChip.rAVIPan.pt動画側開始位置.X, pChip.rAVIPan.pt動画側開始位置.Y, pChip.rAVIPan.pt動画側終了位置.X, pChip.rAVIPan.pt動画側終了位置.Y, pChip.rAVIPan.pt表示側開始位置.X, pChip.rAVIPan.pt表示側開始位置.Y, pChip.rAVIPan.pt表示側終了位置.X, pChip.rAVIPan.pt表示側終了位置.Y, pChip.n総移動時間, pChip.n発声時刻ms);
                                        }
                                        break;
                                }
                            }
                        }
						break;
					#endregion
					#region [ 61-92: 自動再生(BGM, SE) ]
					case 0x61:
					case 0x62:
					case 0x63:
					case 0x64:	// 自動再生(BGM, SE)
					case 0x65:
					case 0x66:
					case 0x67:
					case 0x68:
					case 0x69:
					case 0x70:
					case 0x71:
					case 0x72:
					case 0x73:
					case 0x74:
					case 0x75:
					case 0x76:
					case 0x77:
					case 0x78:
					case 0x79:
					case 0x80:
					case 0x81:
					case 0x82:
					case 0x83:
					case 0x90:
					case 0x91:
					case 0x92:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( configIni.bBGM音を発声する )
							{
								dTX.tWavの再生停止( this.n最後に再生したBGMの実WAV番号[ pChip.nチャンネル番号 - 0x61 ] );
                                dTX.tチップの再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, (int)Eレーン.BGM, dTX.nモニタを考慮した音量(E楽器パート.UNKNOWN));
								this.n最後に再生したBGMの実WAV番号[ pChip.nチャンネル番号 - 0x61 ] = pChip.n整数値・内部番号;
							}
						}
						break;
					#endregion

                    #region [ 84-89: 仮: override sound ]	// #26338 2011.11.8 yyagi
                    case 0x84:	// HH (HO/HC)
					case 0x85:	// CY
					case 0x86:	// RD
					case 0x87:	// LC
					case 0x88:	// Guitar
					case 0x89:	// Bass
					// mute sound (auto)
					// 4A: 84: HH (HO/HC)
					// 4B: 85: CY
					// 4C: 86: RD
					// 4D: 87: LC
					// 2A: 88: Gt
					// AA: 89: Bs

					//	CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Guitar );
					//	CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, 8, n音量, bモニタ, b音程をずらして再生 );
					//	this.n最後に再生した実WAV番号.Guitar = pChip.n整数値・内部番号;

					//	protected void tサウンド再生( CDTX.CChip pChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ, bool b音程をずらして再生 )
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							E楽器パート[] p = { E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.GUITAR, E楽器パート.BASS };

							E楽器パート pp =  p[ pChip.nチャンネル番号 - 0x84 ];
							
//							if ( pp == E楽器パート.DRUMS ) {			// pChip.nチャンネル番号= ..... HHとか、ドラムの場合は変える。
//								//            HC    CY    RD    LC
//								int[] ch = { 0x11, 0x16, 0x19, 0x1A };
//								pChip.nチャンネル番号 = ch[ pChip.nチャンネル番号 - 0x84 ]; 
//							}
                            this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, pp, dTX.nモニタを考慮した音量(pp));
						}
						break;
					#endregion

					#region [ a0-a7: ベース演奏 ]
					case 0xa0:	// ベース演奏
					case 0xa1:
					case 0xa2:
					case 0xa3:
					case 0xa4:
					case 0xa5:
					case 0xa6:
					case 0xa7:
						this.t進行描画・チップ・ギターベース( configIni, ref dTX, ref pChip, E楽器パート.BASS );
						break;
					#endregion
					#region [ a8: ウェイリング(ベース) ]
					case 0xa8:	// ウェイリング(ベース)
						this.t進行描画・チップ・ベース・ウェイリング( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ af: ウェイリングサウンド(ベース) ]
                        /*
					case 0xaf:	// ウェイリングサウンド(ベース)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Bass = pChip;
						}
						break;
                        */
					#endregion
					#region [ b1-b9, bc: 空打ち音設定(ドラム) ]
					case 0xb1:	// 空打ち音設定(ドラム)
					case 0xb2:
					case 0xb3:
					case 0xb4:
					case 0xb5:
					case 0xb6:
					case 0xb7:
					case 0xb8:
					case 0xb9:
					case 0xbc:
						this.t進行描画・チップ・空打ち音設定・ドラム( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ ba: 空打ち音設定(ギター) ]
					case 0xba:	// 空打ち音設定(ギター)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の空うちギターChip = pChip;
							pChip.nチャンネル番号 = 0x20;
						}
						break;
					#endregion
					#region [ bb: 空打ち音設定(ベース) ]
					case 0xbb:	// 空打ち音設定(ベース)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の空うちベースChip = pChip;
							pChip.nチャンネル番号 = 0xA0;
						}
						break;
					#endregion
					#region [ c4, c7, d5-d9, e0: BGA画像入れ替え ]
					case 0xc4:
					case 0xc7:
					case 0xd5:
					case 0xd6:	// BGA画像入れ替え
					case 0xd7:
					case 0xd8:
					case 0xd9:
					case 0xe0:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( ( configIni.bBGA有効 && ( pChip.eBGA種別 == EBGA種別.BMP ) ) || ( pChip.eBGA種別 == EBGA種別.BMPTEX ) )
							{
								for ( int i = 0; i < 8; i++ )
								{
									if ( this.nBGAスコープチャンネルマップ[ 0, i ] == pChip.nチャンネル番号 )
									{
										this.actBGA.ChangeScope( this.nBGAスコープチャンネルマップ[ 1, i ], pChip.rBMP, pChip.rBMPTEX );
									}
								}
							}
						}
						break;
					#endregion
                    #region [ da: ミキサーへチップ音追加 ]
                    case 0xDA:
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            //Debug.WriteLine("[DA(AddMixer)] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString("x2") + ", wav=" + pChip.n整数値.ToString("x2") + ", time=" + pChip.n発声時刻ms);
                            pChip.bHit = true;
                            if (listWAV.ContainsKey(pChip.n整数値・内部番号)) // 参照が遠いので後日最適化する
                            {
                                CDTX.CWAV wc = listWAV[pChip.n整数値・内部番号];
                                for (int i = 0; i < nPolyphonicSounds; i++)
                                {
                                    if (wc.rSound[i] != null)
                                    {
                                        //CDTXMania.Sound管理.AddMixer( wc.rSound[ i ] );
                                        AddMixer(wc.rSound[i]);
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region [ db: ミキサーからチップ音削除 ]
                    case 0xDB:
                        if (!pChip.bHit && (pChip.nバーからの距離dot.Drums < 0))
                        {
                            //Debug.WriteLine("[DB(RemoveMixer)] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString("x2") + ", wav=" + pChip.n整数値.ToString("x2") + ", time=" + pChip.n発声時刻ms);
                            pChip.bHit = true;
                            if (listWAV.ContainsKey(pChip.n整数値・内部番号))	// 参照が遠いので後日最適化する
                            {
                                CDTX.CWAV wc = listWAV[pChip.n整数値・内部番号];
                                for (int i = 0; i < nPolyphonicSounds; i++)
                                {
                                    if (wc.rSound[i] != null)
                                    {
                                        //CDTXMania.Sound管理.RemoveMixer( wc.rSound[ i ] );
                                        RemoveMixer(wc.rSound[i]);
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region [ その他(未定義) ]
                    default:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion 
				}
			}
			return false;
		}

        public bool bCheckAutoPlay(CDTX.CChip pChip)
        {
            bool bPChipIsAutoPlay = false;
            bool bGtBsR = false;
            bool bGtBsG = false;
            bool bGtBsB = false;
            bool bGtBsY = false;
            bool bGtBsP = false;
            bool bGtBsW = false;
            bool bGtBsO = false;

            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            switch (pChip.nチャンネル番号)
            {
                case 0x20:
                    bGtBsO = true;
                    break;
                case 0x21:
                    bGtBsB = true;
                    break;
                case 0x22:
                    bGtBsG = true;
                    break;
                case 0x23:
                    bGtBsG = true;
                    bGtBsB = true;
                    break;
                case 0x24:
                    bGtBsR = true;
                    break;
                case 0x25:
                    bGtBsR = true;
                    bGtBsB = true;
                    break;
                case 0x26:
                    bGtBsR = true;
                    bGtBsG = true;
                    break;
                case 0x27:
                    bGtBsR = true;
                    bGtBsG = true;
                    bGtBsB = true;
                    break;
                case 0x28:
                    bGtBsW = true;
                    break;
                default:
                    switch (pChip.nチャンネル番号)
                    {
                        case 0x93:
                            bGtBsY = true;
                            break;
                        case 0x94:
                            bGtBsB = true;
                            bGtBsY = true;
                            break;
                        case 0x95:
                            flag2 = true;
                            flag4 = true;
                            break;
                        case 0x96:
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 0x97:
                            flag = true;
                            flag4 = true;
                            break;
                        case 0x98:
                            flag = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 0x99:
                            flag = true;
                            flag2 = true;
                            flag4 = true;
                            break;
                        case 0x9A:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 0x9B:
                            bGtBsP = true;
                            break;
                        case 156:
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 157:
                            flag2 = true;
                            flag5 = true;
                            break;
                        case 158:
                            flag2 = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 159:
                            flag = true;
                            flag5 = true;
                            break;
                        case 0xA0:
                            bGtBsO = true;
                            break;
                        case 161:
                            flag3 = true;
                            break;
                        case 162:
                            flag2 = true;
                            break;
                        case 163:
                            flag2 = true;
                            flag3 = true;
                            break;
                        case 164:
                            flag = true;
                            break;
                        case 165:
                            flag = true;
                            flag3 = true;
                            break;
                        case 166:
                            flag = true;
                            flag2 = true;
                            break;
                        case 167:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            break;
                        case 168:
                            flag6 = true;
                            break;
                        case 169:
                            flag = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 170:
                            flag = true;
                            flag2 = true;
                            flag5 = true;
                            break;
                        case 171:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 172:
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 173:
                            flag3 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 174:
                            flag2 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 175:
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 197:
                            flag4 = true;
                            break;
                        case 198:
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 200:
                            flag2 = true;
                            flag4 = true;
                            break;
                        case 201:
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 202:
                            flag = true;
                            flag4 = true;
                            break;
                        case 203:
                            flag = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 204:
                            flag = true;
                            flag2 = true;
                            flag4 = true;
                            break;
                        case 205:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            break;
                        case 206:
                            flag5 = true;
                            break;
                        case 207:
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 208:
                            flag = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 209:
                            flag = true;
                            flag3 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 210:
                            flag = true;
                            flag2 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 211:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 218:
                            flag2 = true;
                            flag5 = true;
                            break;
                        case 219:
                            flag2 = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 220:
                            flag = true;
                            flag5 = true;
                            break;
                        case 221:
                            flag = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 222:
                            flag = true;
                            flag2 = true;
                            flag5 = true;
                            break;
                        case 223:
                            flag = true;
                            flag2 = true;
                            flag3 = true;
                            flag5 = true;
                            break;
                        case 225:
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 226:
                            flag3 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 227:
                            flag2 = true;
                            flag4 = true;
                            flag5 = true;
                            break;
                        case 0xE4:
                            bGtBsG = true;
                            bGtBsB = true;
                            bGtBsY = true;
                            bGtBsP = true;
                            break;
                        case 0xE5:
                            bGtBsR = true;
                            bGtBsY = true;
                            bGtBsP = true;
                            break;
                        case 0xE6:
                            bGtBsR = true;
                            bGtBsB = true;
                            bGtBsY = true;
                            bGtBsP = true;
                            break;
                        case 0xE7:
                            bGtBsR = true;
                            bGtBsG = true;
                            bGtBsY = true;
                            bGtBsP = true;
                            break;
                        case 0xE8:
                            bGtBsR = true;
                            bGtBsG = true;
                            bGtBsB = true;
                            bGtBsY = true;
                            bGtBsP = true;
                            break;
                    }
                    break;
            }
            if (pChip.e楽器パート == E楽器パート.DRUMS)
            {
                if (bIsAutoPlay[this.nチャンネル0Atoレーン07[pChip.nチャンネル番号 - 0x11]])
                {
                    bPChipIsAutoPlay = true;
                }
            }
            else if (pChip.e楽器パート == E楽器パート.GUITAR)
            {
                //Trace.TraceInformation( "chip:{0}{1}{2} ", bGtBsR, bGtBsG, bGtBsB );
                //Trace.TraceInformation( "auto:{0}{1}{2} ", bIsAutoPlay[ (int) Eレーン.GtR ], bIsAutoPlay[ (int) Eレーン.GtG ], bIsAutoPlay[ (int) Eレーン.GtB ]);
                bPChipIsAutoPlay = true;
                if (bIsAutoPlay[(int)Eレーン.GtPick] == false) bPChipIsAutoPlay = false;
                else
                {
                    if (bGtBsR == true && bIsAutoPlay[(int)Eレーン.GtR] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsG == true && bIsAutoPlay[(int)Eレーン.GtG] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsB == true && bIsAutoPlay[(int)Eレーン.GtB] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsY == true && bIsAutoPlay[(int)Eレーン.GtY] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsP == true && bIsAutoPlay[(int)Eレーン.GtP] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsW == true && bIsAutoPlay[(int)Eレーン.GtW] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsO == true &&
                        (bIsAutoPlay[(int)Eレーン.GtR] == false || bIsAutoPlay[(int)Eレーン.GtG] == false || bIsAutoPlay[(int)Eレーン.GtB] == false || bIsAutoPlay[(int)Eレーン.GtY] == false || bIsAutoPlay[(int)Eレーン.GtP] == false))
                        bPChipIsAutoPlay = false;
                }
                //Trace.TraceInformation( "{0:x2}: {1}", pChip.nチャンネル番号, bPChipIsAutoPlay.ToString() );
            }
            else if (pChip.e楽器パート == E楽器パート.BASS)
            {
                bPChipIsAutoPlay = true;
                if (bIsAutoPlay[(int)Eレーン.BsPick] == false) bPChipIsAutoPlay = false;
                else
                {
                    if (bGtBsR == true && bIsAutoPlay[(int)Eレーン.BsR] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsG == true && bIsAutoPlay[(int)Eレーン.BsG] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsB == true && bIsAutoPlay[(int)Eレーン.BsB] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsW == true && bIsAutoPlay[(int)Eレーン.BsW] == false) bPChipIsAutoPlay = false;
                    else if (bGtBsO == true &&
                        (bIsAutoPlay[(int)Eレーン.BsR] == false || bIsAutoPlay[(int)Eレーン.BsG] == false || bIsAutoPlay[(int)Eレーン.BsB] == false))
                        bPChipIsAutoPlay = false;
                }
            }
            return bPChipIsAutoPlay;
        }


		protected abstract void t進行描画・チップ・ドラムス( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		//protected abstract void t進行描画・チップ・ギター( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected abstract void t進行描画・チップ・ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst );

		protected void t進行描画・チップ・ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst,
			int barYNormal, int barYReverse, int showRangeY0, int showRangeY1, int openXg, int openXb,
			int rectOpenOffsetX, int rectOpenOffsetY, int openChipWidth, int chipHeight,
			int chipWidth, int guitarNormalX, int guitarLeftyX, int bassNormalX, int bassLeftyX, int drawDeltaX, int chipTexDeltaX )
		{
			int instIndex = (int) inst;
			if ( configIni.bGuitar有効 )
			{
				#region [ Hidden/Sudden処理 ]
				if ( configIni.bSudden[ instIndex ] )
				{
					pChip.b可視 = pChip.nバーからの距離dot[ instIndex ] < 200;
				}
				if ( configIni.bHidden[ instIndex ] && ( pChip.nバーからの距離dot[ instIndex ] < 100 ) )
				{
					pChip.b可視 = false;
				}
				#endregion

				bool bChipHasR = false;
				bool bChipHasG = false;
				bool bChipHasB = false;
                bool bChipHasY = false;
                bool bChipHasP = false;
				bool bChipHasW = false;
				bool bChipIsO = false;
                int nチャンネル番号 = pChip.nチャンネル番号;

                switch (nチャンネル番号)
                {
                    case 0x20:
                        bChipIsO = true;
                        break;
                    case 0x21:
                        bChipHasB = true;
                        break;
                    case 0x22:
                        bChipHasG = true;
                        break;
                    case 0x23:
                        bChipHasG = true;
                        bChipHasB = true;
                        break;
                    case 0x24:
                        bChipHasR = true;
                        break;
                    case 0x25:
                        bChipHasR = true;
                        bChipHasB = true;
                        break;
                    case 0x26:
                        bChipHasR = true;
                        bChipHasG = true;
                        break;
                    case 0x27:
                        bChipHasR = true;
                        bChipHasG = true;
                        bChipHasB = true;
                        break;
                    case 0x28:
                        break;
                    default:
                        switch (nチャンネル番号)
                        {
                            case 0x93:
                                bChipHasY = true;
                                break;
                            case 0x94:
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0x95:
                                bChipHasG = true;
                                bChipHasY = true;
                                break;
                            case 0x96:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0x97:
                                bChipHasR = true;
                                bChipHasY = true;
                                break;
                            case 0x98:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0x99:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasY = true;
                                break;
                            case 0x9a:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0x9B:
                                bChipHasP = true;
                                break;
                            case 0x9C:
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0x9D:
                                bChipHasG = true;
                                bChipHasP = true;
                                break;
                            case 0x9E:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0x9F:
                                bChipHasR = true;
                                bChipHasP = true;
                                break;
                            case 0xA0:
                                bChipIsO = true;
                                break;
                            case 0xA1:
                                bChipHasB = true;
                                break;
                            case 0xA2:
                                bChipHasG = true;
                                break;
                            case 0xA3:
                                bChipHasG = true;
                                bChipHasB = true;
                                break;
                            case 0xA4:
                                bChipHasR = true;
                                break;
                            case 0xA5:
                                bChipHasR = true;
                                bChipHasB = true;
                                break;
                            case 0xA6:
                                bChipHasR = true;
                                bChipHasB = true;
                                break;
                            case 0xA7:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                break;
                            case 0xA8:
                                bChipHasG = true;
                                bChipHasB = true;
                                break;
                            case 0xA9:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0xAA:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasP = true;
                                break;
                            case 0xAB:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0xAC:
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xAD:
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xAE:
                                bChipHasG = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xAF:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xC6:
                                bChipHasY = true;
                                break;
                            case 0xC7:
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0xC8:
                                bChipHasG = true;
                                bChipHasY = true;
                                break;
                            case 0xC9:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 202:
                                bChipHasR = true;
                                bChipHasY = true;
                                break;
                            case 0xCB:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 204:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasY = true;
                                break;
                            case 205:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                break;
                            case 0xCE:
                                bChipHasP = true;
                                break;
                            case 0xCF:
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0xD0:
                                bChipHasR = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xD1:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xD2:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xD3:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xD4:
                                bChipHasG = true;
                                bChipHasP = true;
                                break;
                            case 0xD5:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 0xD6:
                                bChipHasR = true;
                                bChipHasP = true;
                                break;
                            case 221:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 222:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasP = true;
                                break;
                            case 223:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasP = true;
                                break;
                            case 225:
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 226:
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 227:
                                bChipHasG = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 228:
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xE5:
                                bChipHasR = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xE6:
                                bChipHasR = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xE7:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                            case 0xE8:
                                bChipHasR = true;
                                bChipHasG = true;
                                bChipHasB = true;
                                bChipHasY = true;
                                bChipHasP = true;
                                break;
                        }
                        break;
                }

				#region [ chip描画 ]
				int OPEN = ( inst == E楽器パート.GUITAR ) ? 10 : 10;
				if ( !pChip.bHit && pChip.b可視 )
				{
					int y = configIni.bReverse[ instIndex ] ? ( barYReverse - pChip.nバーからの距離dot[ instIndex ] ) : ( barYNormal + pChip.nバーからの距離dot[ instIndex ] );
					if ( ( showRangeY0 < y ) && ( y < showRangeY1 ) )
					{
						if ( this.txチップ != null )
						{
							int nアニメカウンタ現在の値 = this.ctチップ模様アニメ[ instIndex ].n現在の値;
							if ( bChipIsO )
							{
								int xo = ( inst == E楽器パート.GUITAR ) ? 88 : openXb;
								this.txチップ.t2D描画( CDTXMania.app.Device, xo, y - 2, new Rectangle( 0, 10, 196, 10 ) );
							}
							Rectangle rc = new Rectangle( rectOpenOffsetX, chipHeight, chipWidth, 10 );
							int x;
							if ( inst == E楽器パート.GUITAR )
							{
								x = ( configIni.bLeft.Guitar ) ? guitarLeftyX : guitarNormalX;
							}
							else
							{
								x = ( configIni.bLeft.Bass ) ? bassLeftyX : bassNormalX;
							}
							int deltaX = ( configIni.bLeft[ instIndex ] ) ? -drawDeltaX : +drawDeltaX;

//Trace.TraceInformation( "chip={0:x2}, E楽器パート={1}, x={2}", pChip.nチャンネル番号, inst, x );
							if ( bChipHasR )
							{
                                if (inst == E楽器パート.GUITAR)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 88, y - chipHeight / 2, new Rectangle(0, 0, 38, 10));
                                }
                                else if (inst == E楽器パート.BASS)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 959, y - chipHeight / 2, new Rectangle(0, 0, 38, 10));
                                }
							}
							rc.X += chipTexDeltaX;
							x += deltaX;
							if ( bChipHasG )
							{
                                if (inst == E楽器パート.GUITAR)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 127, y - chipHeight / 2, new Rectangle(38, 0, 38, 10));
                                }
                                else if(inst == E楽器パート.BASS)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 998, y - chipHeight / 2, new Rectangle(38, 0, 38, 10));
                                }
							}
							rc.X += chipTexDeltaX;
							x += deltaX;
							if ( bChipHasB )
							{
                                if (inst == E楽器パート.GUITAR)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 166, y - chipHeight / 2, new Rectangle(76, 0, 38, 10));
                                }
                                else if (inst == E楽器パート.BASS)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 1037, y - chipHeight / 2, new Rectangle(76, 0, 38, 10));
                                }
							}
                            rc.X += chipTexDeltaX;
                            x += deltaX;
                            if ( bChipHasY )
                            {
                                if (inst == E楽器パート.GUITAR)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 205, y - chipHeight / 2, new Rectangle(114, 0, 38, 10));
                                }
                                else if (inst == E楽器パート.BASS)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 1076, y - chipHeight / 2, new Rectangle(114, 0, 38, 10));
                                }
                            }
                            rc.X += chipTexDeltaX;
                            x += deltaX;
                            if ( bChipHasP )
                            {
                                if (inst == E楽器パート.GUITAR)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 244, y - chipHeight / 2, new Rectangle(152, 0, 38, 10));
                                }
                                else if (inst == E楽器パート.BASS)
                                {
                                    this.txチップ.t2D描画(CDTXMania.app.Device, 1115, y - chipHeight / 2, new Rectangle(152, 0, 38, 10));
                                }
                            }
						}
					}
				}
				#endregion
				//if ( ( configIni.bAutoPlay.Guitar && !pChip.bHit ) && ( pChip.nバーからの距離dot.Guitar < 0 ) )
				if ( ( !pChip.bHit ) && ( pChip.nバーからの距離dot[ instIndex ] < 0 ) )
				{
					int lo = ( inst == E楽器パート.GUITAR ) ? 0 : 5;	// lane offset
					bool autoR = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtR : bIsAutoPlay.BsR;
					bool autoG = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtG : bIsAutoPlay.BsG;
					bool autoB = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtB : bIsAutoPlay.BsB;
                    bool autoY = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtY : bIsAutoPlay.BsY;
                    bool autoP = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtP : bIsAutoPlay.BsP;
					bool autoPick = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtPick : bIsAutoPlay.BsPick;
					bool pushingR = CDTXMania.Pad.b押されている( inst, Eパッド.R );
					bool pushingG = CDTXMania.Pad.b押されている( inst, Eパッド.G );
					bool pushingB = CDTXMania.Pad.b押されている( inst, Eパッド.B );
                    bool pushingY = CDTXMania.Pad.b押されている( inst, Eパッド.Y );
                    bool pushingP = CDTXMania.Pad.b押されている( inst, Eパッド.P );

					#region [ Chip Fire effects ]
					bool bSuccessOPEN = bChipIsO && ( autoR || !pushingR ) && ( autoG || !pushingG ) && ( autoB || !pushingB );
					if ( ( bChipHasR && ( autoR || pushingR ) && autoPick ) || bSuccessOPEN )
					{
						this.actChipFireGB.Start( 0 + lo );
					}
					if ( ( bChipHasG && ( autoG || pushingG ) && autoPick ) || bSuccessOPEN )
					{
						this.actChipFireGB.Start( 1 + lo );
					}
					if ( ( bChipHasB && ( autoB || pushingB ) && autoPick ) || bSuccessOPEN )
					{
						this.actChipFireGB.Start( 2 + lo );
					}
                    if ( ( bChipHasY && ( autoY || pushingY ) && autoPick ) || bSuccessOPEN )
					{
						this.actChipFireGB.Start( 3 + lo );
					}
                    if ( ( bChipHasP && ( autoP || pushingP ) && autoPick ) || bSuccessOPEN )
					{
						this.actChipFireGB.Start( 4 + lo );
					}
					#endregion
					if ( autoPick )
					{
						bool bMiss = true;
						if ( bChipHasR == autoR && bChipHasG == autoG && bChipHasB == autoB && bChipHasY == autoY && bChipHasP == autoP )		// autoレーンとチップレーン一致時はOK
						{																			// この条件を加えないと、同時に非autoレーンを押下している時にNGとなってしまう。
							bMiss = false;
						}
						else if ( ( autoR || ( bChipHasR == pushingR ) ) && ( autoG || ( bChipHasG == pushingG ) ) && ( autoB || ( bChipHasB == pushingB ) ) && ( autoY || ( bChipHasY == pushingY ) ) && ( autoP || bChipHasP == pushingP ) )
							// ( bChipHasR == ( pushingR | autoR ) ) && ( bChipHasG == ( pushingG | autoG ) ) && ( bChipHasB == ( pushingB | autoB ) ) )
						{
							bMiss = false;
						}
						else if ( ( ( bChipIsO == true ) && ( !pushingR | autoR ) && ( !pushingG | autoG ) && ( !pushingB | autoB ) && ( !pushingY | autoY) && ( !pushingP | autoP) ) )	// OPEN時
						{
							bMiss = false;
						}
						pChip.bHit = true;
						this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, inst, dTX.nモニタを考慮した音量( inst ), false, bMiss );
						this.r次にくるギターChip = null;
						if ( !bMiss )
						{
							this.tチップのヒット処理( pChip.n発声時刻ms, pChip );
						}
						else
						{
							pChip.nLag = 0;		// tチップのヒット処理()の引数最後がfalseの時はpChip.nLagを計算しないため、ここでAutoPickかつMissのLag=0を代入
							this.tチップのヒット処理( pChip.n発声時刻ms, pChip, false );
						}
						int chWailingChip = ( inst == E楽器パート.GUITAR ) ? 0x28 : 0xA8;
						CDTX.CChip item = this.r指定時刻に一番近い未ヒットChip( pChip.n発声時刻ms, chWailingChip, this.nInputAdjustTimeMs[ instIndex ], 140 );
						if ( item != null && !bMiss )
						{
							this.queWailing[ instIndex ].Enqueue( item );
						}
					}
				}
				return;
			}	// end of "if configIni.bGuitar有効"
			if ( !pChip.bHit && ( pChip.nバーからの距離dot[ instIndex ] < 0 ) )	// Guitar/Bass無効の場合は、自動演奏する
			{
				pChip.bHit = true;
				this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, inst, dTX.nモニタを考慮した音量( inst ) );
			}
		}

		
		protected virtual void t進行描画・チップ・ギターベース・ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst )
		{
			int indexInst = (int) inst;
			if ( configIni.bGuitar有効 )
			{
				#region [ Sudden/Hidden処理 ]
				if ( configIni.bSudden[indexInst] )
				{
					pChip.b可視 = pChip.nバーからの距離dot[indexInst] < 200;
				}
				if ( configIni.bHidden[indexInst] && ( pChip.nバーからの距離dot[indexInst] < 100 ) )
				{
					pChip.b可視 = false;
				}
				#endregion
				//
				// ここにチップ更新処理が入る(overrideで入れる)。といっても座標とチップサイズが違うだけで処理はまるまる同じ。
				//
				if ( !pChip.bHit && ( pChip.nバーからの距離dot[indexInst] < 0 ) )
				{
					if ( pChip.nバーからの距離dot[indexInst] < -234 )	// #25253 2011.5.29 yyagi: Don't set pChip.bHit=true for wailing at once. It need to 1sec-delay (234pix per 1sec). 
					{
						pChip.bHit = true;
					}
					bool autoW = ( inst == E楽器パート.GUITAR ) ? configIni.bAutoPlay.GtW : configIni.bAutoPlay.BsW;
					//if ( configIni.bAutoPlay[ ((int) Eレーン.Guitar - 1) + indexInst ] )	// このような、バグの入りやすい書き方(GT/BSのindex値が他と異なる)はいずれ見直したい
					if ( autoW )
					{
					//    pChip.bHit = true;								// #25253 2011.5.29 yyagi: Set pChip.bHit=true if autoplay.
					//    this.actWailingBonus.Start( inst, this.r現在の歓声Chip[indexInst] );
					// #23886 2012.5.22 yyagi; To support auto Wailing; Don't do wailing for ALL wailing chips. Do wailing for queued wailing chip.
					// wailing chips are queued when 1) manually wailing and not missed at that time 2) AutoWailing=ON and not missed at that time
						long nTimeStamp_Wailed = pChip.n発声時刻ms + CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
						DoWailingFromQueue( inst, nTimeStamp_Wailed, autoW );
					}
				}
				return;
			}
			pChip.bHit = true;
		}
		protected virtual void t進行描画・チップ・ギター・ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			t進行描画・チップ・ギターベース・ウェイリング( configIni, ref dTX, ref pChip, E楽器パート.GUITAR );
		}
		protected abstract void t進行描画・チップ・フィルイン( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
        protected abstract void t進行描画・チップ・ボーナス(CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip);
        protected void t進行描画・フィルインエフェクト()
        {
            this.actFillin.On進行描画();
        }
        protected abstract void t進行描画・チップ・小節線( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		//protected abstract void t進行描画・チップ・ベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected virtual void t進行描画・チップ・ベース・ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip )
		{
			t進行描画・チップ・ギターベース・ウェイリング( configIni, ref dTX, ref pChip, E楽器パート.BASS );
		}
		protected abstract void t進行描画・チップ・空打ち音設定・ドラム( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected void t進行描画・チップアニメ()
		{
			for ( int i = 0; i < 3; i++ )			// 0=drums, 1=guitar, 2=bass
			{
				if ( this.ctチップ模様アニメ[ i ] != null )
				{
					this.ctチップ模様アニメ[ i ].t進行Loop();
				}
			}
			if ( this.ctWailingチップ模様アニメ != null )
			{
				this.ctWailingチップ模様アニメ.t進行Loop();
			}
            if (this.ctBPMバー != null)
            {
                this.ctBPMバー.t進行Loop();
                this.ctコンボ動作タイマ.t進行Loop();
            }
            if(CDTXMania.ConfigIni.bDrums有効)  //2013.05.16.kairera0467 ギター側のアニメーションは未実装なのでとりあえず。
                this.ct登場用.t進行();
		}

		protected bool t進行描画・フェードイン・アウト()
		{
			switch ( base.eフェーズID )
			{
				case CStage.Eフェーズ.共通_フェードイン:
					if ( this.actFI.On進行描画() != 0 )
					{
						base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
					}
					break;

				case CStage.Eフェーズ.共通_フェードアウト:
				case CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト:
					if ( this.actFO.On進行描画() != 0 )
					{
						return true;
					}
					break;

				case CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト:
                    if (this.actFOStageClear.On進行描画() == 0)
                    {
                        break;
                    }
					return true;
		
			}
			return false;
		}
		protected void t進行描画・レーンフラッシュD()
		{
			if ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actLaneFlushD.On進行描画();
			}
		}
		protected void t進行描画・レーンフラッシュGB()
		{
			if ( CDTXMania.ConfigIni.bGuitar有効 )
			{
				this.actLaneFlushGB.On進行描画();
			}
		}
		protected abstract void t進行描画・演奏情報();
		protected void t進行描画・演奏情報(int x, int y)
		{
			if ( !CDTXMania.ConfigIni.b演奏情報を表示しない )
			{
				this.actPlayInfo.t進行描画( x, y );
			}
		}
        protected void t進行描画・背景()
        {

            if ( this.tx背景 != null )
            {
                this.tx背景.t2D描画(CDTXMania.app.Device, 0, 0);
            }
            //CDTXMania.app.Device.Clear( ClearFlags.ZBuffer | ClearFlags.Target, Color.Black, 0f, 0 );
        }

		protected void t進行描画・判定ライン()
		{
            if (CDTXMania.ConfigIni.bDrums有効)
            {
                if (CDTXMania.ConfigIni.bJudgeLineDisp.Drums == true)
                {
                    int y = CDTXMania.ConfigIni.bReverse.Drums ? 159 : nJudgeLinePosY;
                    this.txヒットバー.t2D描画(CDTXMania.app.Device, 295, y, new Rectangle(0, 0, 0x22f, 6));
                }
            }
		}

		protected void t進行描画・判定文字列()
		{
			this.actJudgeString.On進行描画();
		}
		protected void t進行描画・判定文字列1・通常位置指定の場合()
		{
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) != E判定文字表示位置.判定ライン上または横 )
			{
				this.actJudgeString.On進行描画();
			}
		}
		protected void t進行描画・判定文字列2・判定ライン上指定の場合()
		{
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) == E判定文字表示位置.判定ライン上または横 )
			{
				this.actJudgeString.On進行描画();
			}
		}

		protected void t進行描画・譜面スクロール速度()
		{
			this.act譜面スクロール速度.On進行描画();
		}

		protected abstract void t背景テクスチャの生成();



		protected void t背景テクスチャの生成( string DefaultBgFilename, Rectangle bgrect, string bgfilename )
		{
			Bitmap image = null;
			bool flag = true;

			if ( bgfilename != null && File.Exists( bgfilename ) )
			{
				try
				{
					Bitmap bitmap2 = null;
					bitmap2 = new Bitmap( bgfilename );
					if ( ( bitmap2.Size.Width == 0 ) && ( bitmap2.Size.Height == 0 ) )
					{
						this.tx背景 = null;
						return;
					}
					Bitmap bitmap3 = new Bitmap(SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height);
					Graphics graphics = Graphics.FromImage( bitmap3 );
					for ( int i = 0; i < SampleFramework.GameWindowSize.Height; i += bitmap2.Size.Height )
					{
						for ( int j = 0; j < SampleFramework.GameWindowSize.Width; j += bitmap2.Size.Width )
						{
							graphics.DrawImage( bitmap2, j, i, bitmap2.Width, bitmap2.Height );
						}
					}
					graphics.Dispose();
					bitmap2.Dispose();
					image = new Bitmap( CSkin.Path( DefaultBgFilename ) );
					graphics = Graphics.FromImage( image );
					ColorMatrix matrix2 = new ColorMatrix();
					matrix2.Matrix00 = 1f;
					matrix2.Matrix11 = 1f;
					matrix2.Matrix22 = 1f;
					matrix2.Matrix33 = ( (float) CDTXMania.ConfigIni.n背景の透過度 ) / 255f;
					matrix2.Matrix44 = 1f;
					ColorMatrix newColorMatrix = matrix2;
					ImageAttributes imageAttr = new ImageAttributes();
					imageAttr.SetColorMatrix( newColorMatrix );
					graphics.DrawImage( bitmap3, new Rectangle( 0, 0, SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height ), 0, 0, SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height, GraphicsUnit.Pixel, imageAttr );
					imageAttr.Dispose();
					graphics.DrawImage( bitmap3, bgrect, bgrect.X, bgrect.Y, bgrect.Width, bgrect.Height, GraphicsUnit.Pixel );
					graphics.Dispose();
					bitmap3.Dispose();
					flag = false;
				}
				catch
				{
					Trace.TraceError( "背景画像の読み込みに失敗しました。({0})", new object[] { bgfilename } );
				}
			}
			if ( flag )
			{
				bgfilename = CSkin.Path( DefaultBgFilename );
				try
				{
					image = new Bitmap( bgfilename );
				}
				catch
				{
					Trace.TraceError( "背景画像の読み込みに失敗しました。({0})", new object[] { bgfilename } );
					this.tx背景 = null;
					return;
				}
			}
			if ( ( CDTXMania.DTX.listBMP.Count > 0 ) || ( CDTXMania.DTX.listBMPTEX.Count > 0 ) )
			{
				Graphics graphics2 = Graphics.FromImage( image );
				graphics2.FillRectangle( Brushes.Black, bgrect.X, bgrect.Y, bgrect.Width, bgrect.Height );
				graphics2.Dispose();
			}
			try
			{
				this.tx背景 = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
			}
			catch ( CTextureCreateFailedException )
			{
				Trace.TraceError( "背景テクスチャの生成に失敗しました。" );
				this.tx背景 = null;
			}
			image.Dispose();
		}

		protected virtual void t入力処理・ギター()
		{
			t入力処理・ギターベース( E楽器パート.GUITAR );
		}
		protected virtual void t入力処理・ベース()
		{
			t入力処理・ギターベース( E楽器パート.BASS );
		}


		protected virtual void t入力処理・ギターベース(E楽器パート inst)
		{
			int indexInst = (int) inst;
			#region [ スクロール速度変更 ]
			if ( CDTXMania.Pad.b押されている( inst, Eパッド.Decide ) && CDTXMania.Pad.b押された( inst, Eパッド.B ) )
			{
				CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] = Math.Min( CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] + 1, 0x7cf );
			}
			if ( CDTXMania.Pad.b押されている( inst, Eパッド.Decide ) && CDTXMania.Pad.b押された( inst, Eパッド.R ) )
			{
				CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] = Math.Max( CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] - 1, 0 );
			}
			#endregion

			if ( !CDTXMania.ConfigIni.bGuitar有効 || !CDTXMania.DTX.bチップがある[indexInst] )
			{
				return;
			}

			int R = ( inst == E楽器パート.GUITAR ) ? 0 : 5;
			int G = R + 1;
			int B = R + 2;
            int Y = R + 3;
            int P = R + 4;
			bool autoW =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtW : bIsAutoPlay.BsW;
			bool autoR =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtR : bIsAutoPlay.BsR;
			bool autoG =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtG : bIsAutoPlay.BsG;
			bool autoB =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtB : bIsAutoPlay.BsB;
            bool autoY =    ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtY : bIsAutoPlay.BsY;
            bool autoP =    ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtP : bIsAutoPlay.BsP;
			bool autoPick =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtPick : bIsAutoPlay.BsPick;
			int nAutoW = ( autoW ) ? 8 : 0;
			int nAutoR = ( autoR ) ? 4 : 0;
			int nAutoG = ( autoG ) ? 2 : 0;
			int nAutoB = ( autoB ) ? 1 : 0;
            int nAutoY = ( autoY ) ? 16: 0;
            int nAutoP = ( autoP ) ? 32: 0;
			int nAutoMask = nAutoW | nAutoR | nAutoG | nAutoB | nAutoY | nAutoP;

//			if ( bIsAutoPlay[ (int) Eレーン.Guitar - 1 + indexInst ] )	// このような、バグの入りやすい書き方(GT/BSのindex値が他と異なる)はいずれ見直したい
//			{
			CDTX.CChip chip = this.r次に来る指定楽器Chipを更新して返す(inst);
            if (chip != null)
            {
                bool flag7 = false;
                bool flag8 = false;
                bool flag9 = false;
                bool flag10 = false;
                bool flag11 = false;
                int nチャンネル番号 = chip.nチャンネル番号;
                switch (nチャンネル番号)
                {
                    case 0x20:
                        break;
                    case 0x21:
                        flag9 = true;
                        break;
                    case 0x22:
                        flag8 = true;
                        break;
                    case 0x23:
                        flag8 = true;
                        flag9 = true;
                        break;
                    case 0x24:
                        flag7 = true;
                        break;
                    case 0x25:
                        flag7 = true;
                        flag9 = true;
                        break;
                    case 0x26:
                        flag7 = true;
                        flag8 = true;
                        break;
                    case 0x27:
                        flag7 = true;
                        flag8 = true;
                        flag9 = true;
                        break;
                    default:
                        switch (nチャンネル番号)
                        {
                            case 147:
                                flag10 = true;
                                break;
                            case 148:
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 149:
                                flag8 = true;
                                flag10 = true;
                                break;
                            case 150:
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 151:
                                flag7 = true;
                                flag10 = true;
                                break;
                            case 152:
                                flag7 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 153:
                                flag7 = true;
                                flag8 = true;
                                flag10 = true;
                                break;
                            case 154:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 155:
                                flag11 = true;
                                break;
                            case 156:
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 157:
                                flag8 = true;
                                flag11 = true;
                                break;
                            case 158:
                                flag8 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 159:
                                flag7 = true;
                                flag11 = true;
                                break;
                            case 161:
                                flag9 = true;
                                break;
                            case 162:
                                flag8 = true;
                                break;
                            case 163:
                                flag8 = true;
                                flag9 = true;
                                break;
                            case 164:
                                flag7 = true;
                                break;
                            case 165:
                                flag7 = true;
                                flag9 = true;
                                break;
                            case 166:
                                flag7 = true;
                                flag8 = true;
                                break;
                            case 167:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                break;
                            case 169:
                                flag7 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 170:
                                flag7 = true;
                                flag8 = true;
                                flag11 = true;
                                break;
                            case 171:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 172:
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 173:
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 174:
                                flag8 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 175:
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 197:
                                flag10 = true;
                                break;
                            case 198:
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 200:
                                flag8 = true;
                                flag10 = true;
                                break;
                            case 201:
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 202:
                                flag7 = true;
                                flag10 = true;
                                break;
                            case 203:
                                flag7 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 204:
                                flag7 = true;
                                flag8 = true;
                                flag10 = true;
                                break;
                            case 205:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                break;
                            case 206:
                                flag11 = true;
                                break;
                            case 207:
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 208:
                                flag7 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 209:
                                flag7 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 210:
                                flag7 = true;
                                flag8 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 211:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 218:
                                flag8 = true;
                                flag11 = true;
                                break;
                            case 219:
                                flag8 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 220:
                                flag7 = true;
                                flag11 = true;
                                break;
                            case 221:
                                flag7 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 222:
                                flag7 = true;
                                flag8 = true;
                                flag11 = true;
                                break;
                            case 223:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag11 = true;
                                break;
                            case 225:
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 226:
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 227:
                                flag8 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 228:
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 229:
                                flag7 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 230:
                                flag7 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 231:
                                flag7 = true;
                                flag8 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                            case 232:
                                flag7 = true;
                                flag8 = true;
                                flag9 = true;
                                flag10 = true;
                                flag11 = true;
                                break;
                        }
                        break;
                }
                if (flag7 && autoR)
                {
                    this.actLaneFlushGB.Start(R);
                    this.actRGB.Push(R);
                }
                if (flag8 && autoG)
                {
                    this.actLaneFlushGB.Start(G);
                    this.actRGB.Push(G);
                }
                if (flag9 && autoB)
                {
                    this.actLaneFlushGB.Start(B);
                    this.actRGB.Push(B);
                }
                if (flag10 && autoY)
                {
                    this.actLaneFlushGB.Start(Y);
                    this.actRGB.Push(Y);
                }
                if (flag11 && autoP)
                {
                    this.actLaneFlushGB.Start(P);
                    this.actRGB.Push(P);
                }
            }
//			else
			{
				int pushingR = CDTXMania.Pad.b押されている( inst, Eパッド.R ) ? 4 : 0;
				this.t入力メソッド記憶( inst );
				int pushingG = CDTXMania.Pad.b押されている( inst, Eパッド.G ) ? 2 : 0;
				this.t入力メソッド記憶( inst );
				int pushingB = CDTXMania.Pad.b押されている( inst, Eパッド.B ) ? 1 : 0;
				this.t入力メソッド記憶( inst );
                int pushingY = CDTXMania.Pad.b押されている( inst, Eパッド.Y ) ? 16: 0;
                this.t入力メソッド記憶(inst);
                int pushingP = CDTXMania.Pad.b押されている( inst, Eパッド.P ) ? 32: 0;
                this.t入力メソッド記憶(inst);
				int flagRGB = pushingR | pushingG | pushingB | pushingY | pushingP;
				if ( pushingR != 0 )
				{
					this.actLaneFlushGB.Start( R );
					this.actRGB.Push( R );
				}
				if ( pushingG != 0 )
				{
					this.actLaneFlushGB.Start( G );
					this.actRGB.Push( G );
				}
				if ( pushingB != 0 )
				{
					this.actLaneFlushGB.Start( B );
					this.actRGB.Push( B );
				}
                if ( pushingY != 0 )
                {
                    this.actLaneFlushGB.Start( Y );
                    this.actRGB.Push( Y );
                }
                if ( pushingP != 0 )
                {
                    this.actLaneFlushGB.Start( P );
                    this.actRGB.Push( P );
                }
				// auto pickだとここから先に行かないので注意
				List<STInputEvent> events = CDTXMania.Pad.GetEvents( inst, Eパッド.Pick );
				if ( ( events != null ) && ( events.Count > 0 ) )
				{
					foreach ( STInputEvent eventPick in events )
					{
						if ( !eventPick.b押された )
						{
							continue;
						}
						this.t入力メソッド記憶( inst );
						long nTime = eventPick.nTimeStamp - CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
						int chWailingSound = ( inst == E楽器パート.GUITAR ) ? 0x2F : 0xAF;
						CDTX.CChip pChip = this.r指定時刻に一番近い未ヒットChip( nTime, chWailingSound, this.nInputAdjustTimeMs[indexInst] );	// E楽器パート.GUITARなチップ全てにヒットする
						E判定 e判定 = this.e指定時刻からChipのJUDGEを返す( nTime, pChip, this.nInputAdjustTimeMs[indexInst] );
//Trace.TraceInformation("ch={0:x2}, mask1={1:x1}, mask2={2:x2}", pChip.nチャンネル番号,  ( pChip.nチャンネル番号 & ~nAutoMask ) & 0x0F, ( flagRGB & ~nAutoMask) & 0x0F );
						if ( ( pChip != null ) && ( ( ( pChip.nチャンネル番号 & ~nAutoMask ) & 0x0F ) == ( ( flagRGB & ~nAutoMask) & 0x0F ) ) && ( e判定 != E判定.Miss ) )
						{
							bool bChipHasR = false;
							bool bChipHasG = false;
							bool bChipHasB = false;
                            bool bChipHasY = false;
                            bool bChipHasP = false;
							bool bChipHasW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
							bool bChipIsO = false;
                            bool bSuccessOPEN = bChipIsO && (autoR || pushingR == 0) && (autoG || pushingG == 0) && (autoB || pushingB == 0) && (autoY || pushingY == 0) && (autoP || pushingP == 0);

                            if (chip != null)
                            {
                                int nチャンネル番号 = chip.nチャンネル番号;
                                switch (nチャンネル番号)
                                {
                                    case 0x20:
                                        bChipIsO = true;
                                        break;
                                    case 0x21:
                                        bChipHasB = true;
                                        break;
                                    case 0x22:
                                        bChipHasG = true;
                                        break;
                                    case 0x23:
                                        bChipHasG = true;
                                        bChipHasB = true;
                                        break;
                                    case 36:
                                        bChipHasR = true;
                                        break;
                                    case 37:
                                        bChipHasR = true;
                                        bChipHasB = true;
                                        break;
                                    case 38:
                                        bChipHasR = true;
                                        bChipHasG = true;
                                        break;
                                    case 0x27:
                                        bChipHasR = true;
                                        bChipHasG = true;
                                        bChipHasB = true;
                                        break;
                                    case 0x28:
                                        break;
                                    default:
                                        switch (nチャンネル番号)
                                        {
                                            case 0x93:
                                                bChipHasY = true;
                                                break;
                                            case 0x94:
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 0x95:
                                                bChipHasG = true;
                                                bChipHasY = true;
                                                break;
                                            case 150:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 151:
                                                bChipHasR = true;
                                                bChipHasY = true;
                                                break;
                                            case 152:
                                                bChipHasR = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;                
                                            case 153:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasY = true;
                                                break;
                                                
                                            case 154:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 155:
                                                bChipHasP = true;
                                                break;
                                            case 156:
                                                bChipHasB = true;
                                                bChipHasP = true;
                                                break;
                                            case 157:
                                                bChipHasG = true;
                                                bChipHasP = true;
                                                break;
                                            case 158:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasP = true;
                                                break;
                                            case 159:
                                                bChipHasR = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xA0:
                                                bChipIsO = true;
                                                break;
                                            case 161:
                                                bChipHasB = true;
                                                break;
                                            case 162:
                                                bChipHasG = true;
                                                break;
                                            case 163:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                break;
                                            case 164:
                                                bChipHasR = true;
                                                break;
                                                
                                            case 165:
                                                bChipHasR = true;
                                                bChipHasB = true;
                                                break;
                                            case 166:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                break;
                                            case 167:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                break;
                                            
                                            case 0xA8:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                break;
                                            case 0xA9:
                                                bChipHasR = true;
                                                bChipHasB = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xAA:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasP = true;
                                                break;
                                            
                                            case 0xAB:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xAC:
                                                bChipHasY = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xAD:
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xAE:
                                                bChipHasG = true;
                                                bChipHasY = true;
                                                bChipHasP = true;
                                                break;
                                            case 0xAF:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                bChipHasP = true;
                                                break;

                                                //ベース
                                            case 0xC5:
                                                bChipHasY = true;
                                                break;
                                            
                                            case 0xC6:
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 0xC8:
                                                bChipHasG = true;
                                                bChipHasY = true;
                                                break;
                                            case 0xC9:
                                                bChipHasG = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 0xCA:
                                                bChipHasR = true;
                                                bChipHasY = true;
                                                break;
                                            case 0xCB:
                                                bChipHasR = true;
                                                bChipHasB = true;
                                                bChipHasY = true;
                                                break;
                                            case 0xCC:
                                                bChipHasR = true;
                                                bChipHasG = true;
                                                bChipHasY = true;
                                                break;/*
                                            case 0xCD:
                                                flag12 = true;
                                                flag13 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                break;
                                            case 0xCE:
                                                flag16 = true;
                                                break;
                                            case 0xCF:
                                                flag14 = true;
                                                flag16 = true;
                                                break;
                                            case 208:
                                                flag12 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 209:
                                                flag12 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 210:
                                                flag12 = true;
                                                flag13 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 211:
                                                flag12 = true;
                                                flag13 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 218:
                                                flag13 = true;
                                                flag16 = true;
                                                break;
                                            case 219:
                                                flag13 = true;
                                                flag14 = true;
                                                flag16 = true;
                                                break;
                                            case 220:
                                                flag12 = true;
                                                flag16 = true;
                                                break;
                                            case 221:
                                                flag12 = true;
                                                flag14 = true;
                                                flag16 = true;
                                                break;
                                            case 0xDE:
                                                flag12 = true;
                                                flag13 = true;
                                                flag16 = true;
                                                break;
                                            case 0xDF:
                                                flag12 = true;
                                                flag13 = true;
                                                flag14 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE1:
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE2:
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE3:
                                                flag13 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE4:
                                                flag13 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE5:
                                                flag12 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE6:
                                                flag12 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE7:
                                                flag12 = true;
                                                flag13 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                            case 0xE8:
                                                flag12 = true;
                                                flag13 = true;
                                                flag14 = true;
                                                flag15 = true;
                                                flag16 = true;
                                                break;
                                                */
                                        }
                                        break;
                                }
                            }

                            if ((bChipHasR && (autoR || pushingR != 0)) || bChipHasR )
                            {
                                this.actChipFireGB.Start( R );
                            }
                            if ((bChipHasG && (autoG || pushingG != 0)) || bChipHasG )
                            {
                                this.actChipFireGB.Start( G );
                            }
                            if ((bChipHasB && (autoB || pushingB != 0)) || bChipHasB )
                            {
                                this.actChipFireGB.Start( B );
                            }
                            if ((bChipHasY && (autoY || pushingY != 0)) || bChipHasY )
                            {
                                this.actChipFireGB.Start( Y );
                            }
                            if ((bChipHasP && (autoP || pushingP != 0)) || bChipHasP )
                            {
                                this.actChipFireGB.Start( P );
                            }
							this.tチップのヒット処理( nTime, pChip );
							this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.nシステム時刻, inst, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する[indexInst], e判定 == E判定.Poor );
							int chWailingChip = ( inst == E楽器パート.GUITAR ) ? 0x28 : 0xA8;
							CDTX.CChip item = this.r指定時刻に一番近い未ヒットChip( nTime, chWailingChip, this.nInputAdjustTimeMs[ indexInst ], 140 );
							if ( item != null )
							{
								this.queWailing[indexInst].Enqueue( item );
							}
							continue;
						}

						// 以下、間違いレーンでのピック時
						CDTX.CChip NoChipPicked = ( inst == E楽器パート.GUITAR ) ? this.r現在の空うちギターChip : this.r現在の空うちベースChip;
						if ( ( NoChipPicked != null ) || ( ( NoChipPicked = this.r指定時刻に一番近いChip・ヒット未済問わず不可視考慮( nTime, chWailingSound, this.nInputAdjustTimeMs[indexInst] ) ) != null ) )
						{
							this.tサウンド再生( NoChipPicked, CSound管理.rc演奏用タイマ.nシステム時刻, inst, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する[indexInst], true );
						}
						if ( !CDTXMania.ConfigIni.bLight[indexInst] )
						{
							this.tチップのヒット処理・BadならびにTight時のMiss( inst );
						}
					}
				}
				List<STInputEvent> list = CDTXMania.Pad.GetEvents(inst, Eパッド.Wail );
				if ( ( list != null ) && ( list.Count > 0 ) )
				{
					foreach ( STInputEvent eventWailed in list )
					{
						if ( !eventWailed.b押された )
						{
							continue;
						}
						DoWailingFromQueue( inst, eventWailed.nTimeStamp,  autoW );
					}
				}
			}
		}

		private void DoWailingFromQueue( E楽器パート inst, long nTimeStamp_Wailed,  bool autoW )
		{
			int indexInst = (int) inst;
			long nTimeWailed = nTimeStamp_Wailed - CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
			CDTX.CChip chipWailing;
			while ( ( this.queWailing[ indexInst ].Count > 0 ) && ( ( chipWailing = this.queWailing[ indexInst ].Dequeue() ) != null ) )
			{
				if ( ( nTimeWailed - chipWailing.n発声時刻ms ) <= 1000 )		// #24245 2011.1.26 yyagi: 800 -> 1000
				{
					chipWailing.bHit = true;
					this.actWailingBonus.Start( inst, this.r現在の歓声Chip[ indexInst ] );
					//if ( !bIsAutoPlay[indexInst] )
					if ( !autoW )
					{
                        if (CDTXMania.ConfigIni.nSkillMode == 0)
                        {
                            int nCombo = ( this.actCombo.n現在のコンボ数[ indexInst ] < 500 ) ? this.actCombo.n現在のコンボ数[ indexInst ] : 500;
						    this.actScore.Add( inst, bIsAutoPlay, nCombo * 3000L );		// #24245 2011.1.26 yyagi changed DRUMS->BASS, add nCombo conditions
                        }
                        else
                        {
                            int nAddScore = this.actCombo.n現在のコンボ数[indexInst] > 500 ? 50000 : this.actCombo.n現在のコンボ数[indexInst] * 100;
                            this.actScore.Add(inst, bIsAutoPlay, nAddScore);		// #24245 2011.1.26 yyagi changed DRUMS->BASS, add nCombo conditions

                            this.tブーストボーナス();
                        }
					}
				}
			}
		}

        private void tブーストボーナス()
        {

            for (int i = 0; i < 3; i++)
            {
                this.ctタイマー[i] = new CCounter(0, 3000, 1, CDTXMania.Timer);
                if (!this.ctタイマー[i].b停止中)
                {
                    this.bブーストボーナス = true;
                    this.ctタイマー[i].t進行();
                    if(this.ctタイマー[i].b終了値に達した)
                    {
                        this.ctタイマー[i].t停止();
                        this.bブーストボーナス = false;
                    }
                }
            }
        }

        #endregion
	}
}
