﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTXMania
{
	/// <summary>
	/// <para>Activity プラグインへのインターフェース。</para>
	/// <para>DTXMania は、IPluginActivity に対して、以下のライフサイクルを実行します。</para>
	/// <para>(1) DTXMania の起動処理の最後（セットアップ画面が表示される直前）に一度だけ、On初期化()・OnManagedリソースの作成()・OnUnmanagedリソースの作成() をこの順番で1回ずつ呼び出します。</para>
	/// <para>(2) DTXMania の終了処理の最初（Thank you for playing が消えた直後）に一度だけ、OnUnmanagedリソースの解放()・OnManagedリソースの解放()・On終了() をこの順番で1回ずつ呼び出します。</para>
	/// <para>(3) DTXMania の起動中、すなわち(1)～(2)の間は、どんなシーンであっても、常に On進行描画() を1フレームにつき1回ずつ呼び出します。</para>
	/// <para>(4) Direct3D デバイスのリセット時には、デバイスのリセット前に OnUnmanagedリソースの解放() を1回呼び出し、デバイスをリセットしたのち、OnUnmanagedリソースの作成() を1回呼び出します。</para>
	/// <para>(5) Direct3D デバイスのロスト時には、デバイスの再生成前に OnUnmanagedリソースの解放()・OnManagedリソースの解放() を1回ずつ呼び出し、デバイスを再生成したのち、OnManagedリソースの作成()・OnUnmanagedリソースの作成() を1回ずつ呼び出します。</para>
	/// </summary>
    public interface IPluginActivity
	{
		/// <summary>
		/// <para>プラグインの初期化を行います。</para>
		/// <para>DTXMania の起動処理の最後（セットアップ画面が表示される直前）に、DTXMania から一度だけ呼び出されます。</para>
		/// <param name="PluginHost">プラグインが、ホスト（DTXMania）の情報にアクセスするためのオブジェクト。</param>
		/// </summary>
		void On初期化( DTXMania.IPluginHost PluginHost );

		/// <summary>
		/// <para>プラグインの終了処理を行います。</para>
		/// <para>DTXMania の終了処理の最初（Thank you for playing が消えた直後）に、DTXMania から一度だけ呼び出されます。</para>
		/// </summary>
		void On終了();

		/// <summary>
		/// Managed リソースを作成します。
		/// </summary>
		void OnManagedリソースの作成();
		
		/// <summary>
		/// Unmanaged リソースを作成します。
		/// </summary>
		void OnUnmanagedリソースの作成();

		/// <summary>
		/// Unmanaged リソースを解放します。
		/// </summary>
		void OnUnmanagedリソースの解放();

		/// <summary>
		/// Managed リソースを解放します。
		/// </summary>
		void OnManagedリソースの解放();

		/// <summary>
		/// <para>プラグインの進行と描画を行います。</para>
		/// <para>※現在の DTXMania では、進行と描画は分離されていません。</para>
		/// <para>※BeginScene()/EndScene() は DTXMania 側で行うため、プラグイン側では不要です。</para>
		/// <para>※keyboard.tポーリング() は DTXMania 側で行いますのでプラグイン側では行わないで下さい。</para>
		/// <param name="pad">パッド入力。他のプラグインが入力占有中である場合は null が渡されます。</param>
		/// <param name="keyboard">キーボード入力。他のプラグインが入力占有中である場合は null が渡されます。</param>
		/// </summary>
		void On進行描画( DTXMania.CPad pad, FDK.IInputDevice keyboard );

		/// <summary>
		/// <para>ステージが変わる度に呼び出されます。</para>
		/// <para>呼び出しタイミングは、新しいステージの活性化直後かつ描画開始前です。</para>
		/// </summary>
		void Onステージ変更();

		/// <summary>
		/// <para>選曲画面で選択曲が変更された場合に呼び出されます。</para>
		/// <para>同じ set.def に属する曲の難易度が（HH×2で）変更された場合でも呼び出されます。</para>
		/// <para>ただし、選択が曲でない（BOX, BACK, RANDOM など）場合には呼び出されません。</para>
		/// </summary>
		/// <param name="str選択曲ファイル名">選択されている曲のファイルの名前。絶対パス。</param>
		/// <param name="setDef">選択されている曲の set.def の情報。set.def が存在しなければ null を渡します。</param>
		/// <param name="nブロック番号inSetDef">選択されている曲のset.defブロック番号(0～)。setDef.blocks[]の引数です。set.def が存在しなければ負数を渡します。</param>
		/// <param name="n曲番号inブロック">選択されている曲のブロック内の曲番号(0～4)。setDef.blocks[nブロック番号inSetDef].File[]/Label[]の引数です。set.def が存在しなければ負数を渡します。
		/// この番号は、選曲画面でプレイヤーが目にしているレベル（L1～L5）に常に対応するとは限りません。</param>
		void On選択曲変更( string str選択曲ファイル名, DTXMania.CSetDef setDef, int nブロック番号inSetDef, int n曲番号inブロック );

		void On演奏クリア( DTXMania.CScoreIni scoreIni );
		void On演奏失敗( DTXMania.CScoreIni scoreIni );
		void On演奏キャンセル( DTXMania.CScoreIni scoreIni );
	}
}
