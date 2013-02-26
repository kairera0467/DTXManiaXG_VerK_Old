using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelect曲リスト : CActivity
	{
		// プロパティ

        public bool bIsEnumeratingSongs
        {
            get;
            set;
        }
		public bool bスクロール中
		{
			get
			{
				if( this.n目標のスクロールカウンタ == 0 )
				{
					return ( this.n現在のスクロールカウンタ != 0 );
				}
				return true;
			}
		}
		public int n現在のアンカ難易度レベル 
		{
			get;
			private set;
		}
		public int n現在選択中の曲の現在の難易度レベル
		{
			get
			{
				return this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( this.r現在選択中の曲 );
			}
		}
		public Cスコア r現在選択中のスコア
		{
			get
			{
				if( this.r現在選択中の曲 != null )
				{
					return this.r現在選択中の曲.arスコア[ this.n現在選択中の曲の現在の難易度レベル ];
				}
				return null;
			}
		}
		public C曲リストノード r現在選択中の曲 
		{
			get;
			private set;
		}

		public int nスクロールバー相対y座標
		{
			get;
			private set;
		}

		// t選択曲が変更された()内で使う、直前の選曲の保持
		// (前と同じ曲なら選択曲変更に掛かる再計算を省略して高速化するため)
		private C曲リストノード song_last = null;

		
		// コンストラクタ

		public CActSelect曲リスト()
		{
			this.r現在選択中の曲 = null;
			this.n現在のアンカ難易度レベル = 0;
			base.b活性化してない = true;
			this.bIsEnumeratingSongs = false;
		}


		// メソッド

		public int n現在のアンカ難易度レベルに最も近い難易度レベルを返す( C曲リストノード song )
		{
			// 事前チェック。

			if( song == null )
				return this.n現在のアンカ難易度レベル;	// 曲がまったくないよ

			if( song.arスコア[ this.n現在のアンカ難易度レベル ] != null )
				return this.n現在のアンカ難易度レベル;	// 難易度ぴったりの曲があったよ

			if( ( song.eノード種別 == C曲リストノード.Eノード種別.BOX ) || ( song.eノード種別 == C曲リストノード.Eノード種別.BACKBOX ) )
				return 0;								// BOX と BACKBOX は関係無いよ


			// 現在のアンカレベルから、難易度上向きに検索開始。

			int n最も近いレベル = this.n現在のアンカ難易度レベル;

			for( int i = 0; i < 5; i++ )
			{
				if( song.arスコア[ n最も近いレベル ] != null )
					break;	// 曲があった。

				n最も近いレベル = ( n最も近いレベル + 1 ) % 5;	// 曲がなかったので次の難易度レベルへGo。（5以上になったら0に戻る。）
			}


			// 見つかった曲がアンカより下のレベルだった場合……
			// アンカから下向きに検索すれば、もっとアンカに近い曲があるんじゃね？

			if( n最も近いレベル < this.n現在のアンカ難易度レベル )
			{
				// 現在のアンカレベルから、難易度下向きに検索開始。

				n最も近いレベル = this.n現在のアンカ難易度レベル;

				for( int i = 0; i < 5; i++ )
				{
					if( song.arスコア[ n最も近いレベル ] != null )
						break;	// 曲があった。

					n最も近いレベル = ( ( n最も近いレベル - 1 ) + 5 ) % 5;	// 曲がなかったので次の難易度レベルへGo。（0未満になったら4に戻る。）
				}
			}

			return n最も近いレベル;
		}
		public C曲リストノード r指定された曲が存在するリストの先頭の曲( C曲リストノード song )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( song );
			return ( songList == null ) ? null : songList[ 0 ];
		}
		public C曲リストノード r指定された曲が存在するリストの末尾の曲( C曲リストノード song )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( song );
			return ( songList == null ) ? null : songList[ songList.Count - 1 ];
		}

		private List<C曲リストノード> GetSongListWithinMe( C曲リストノード song )
		{
			if ( song.r親ノード == null )					// root階層のノートだったら
			{
				return CDTXMania.Songs管理.list曲ルート;	// rootのリストを返す
			}
			else
			{
				if ( ( song.r親ノード.list子リスト != null ) && ( song.r親ノード.list子リスト.Count > 0 ) )
				{
					return song.r親ノード.list子リスト;
				}
				else
				{
					return null;
				}
			}
		}


		public delegate void DGSortFunc( List<C曲リストノード> songList, E楽器パート eInst, int order, params object[] p);

		public void t曲リストのソート( DGSortFunc sf, E楽器パート eInst, int order, params object[] p )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( this.r現在選択中の曲 );
			if ( songList == null )
			{
			}
			else
			{
				sf( songList, eInst, order, p );
				this.t現在選択中の曲を元に曲バーを再構成する();
			}
		}

		public bool tBOXに入る()
		{
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
			}
			CDTXMania.Skin.SetCurrentSkinSubfolderFullName(
				CDTXMania.Skin.GetSkinSubfolderFullNameFromSkinName( CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) ), false );

			if( ( this.r現在選択中の曲.list子リスト != null ) && ( this.r現在選択中の曲.list子リスト.Count > 0 ) )
			{
				this.r現在選択中の曲 = this.r現在選択中の曲.list子リスト[ 0 ];
				this.t現在選択中の曲を元に曲バーを再構成する();
				this.t選択曲が変更された(false);									// #27648 項目数変更を反映させる
			}
			return ret;
		}
		public bool tBOXを出る()
		{
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
			}
			CDTXMania.Skin.SetCurrentSkinSubfolderFullName(
				( this.r現在選択中の曲.strSkinPath == "" ) ? "" : CDTXMania.Skin.GetSkinSubfolderFullNameFromSkinName( CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) ), false );
			if ( this.r現在選択中の曲.r親ノード != null )
			{
				this.r現在選択中の曲 = this.r現在選択中の曲.r親ノード;
				this.t現在選択中の曲を元に曲バーを再構成する();
				this.t選択曲が変更された(false);									// #27648 項目数変更を反映させる
			}
			return ret;
		}
		public void t現在選択中の曲を元に曲バーを再構成する()
		{
			this.tバーの初期化();
			for( int i = 0; i < 13; i++ )
			{
				this.t曲名バーの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].col文字色 );
			}
		}
		public void t次に移動()
		{
			if( this.r現在選択中の曲 != null )
			{
				this.n目標のスクロールカウンタ += 100;
			}
		}
		public void t前に移動()
		{
			if( this.r現在選択中の曲 != null )
			{
				this.n目標のスクロールカウンタ -= 100;
			}
		}
		public void t難易度レベルをひとつ進める()
		{
			if( ( this.r現在選択中の曲 == null ) || ( this.r現在選択中の曲.nスコア数 <= 1 ) )
				return;		// 曲にスコアが０～１個しかないなら進める意味なし。
			

			// 難易度レベルを＋１し、現在選曲中のスコアを変更する。

			this.n現在のアンカ難易度レベル = this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( this.r現在選択中の曲 );

			for( int i = 0; i < 5; i++ )
			{
				this.n現在のアンカ難易度レベル = ( this.n現在のアンカ難易度レベル + 1 ) % 5;	// ５以上になったら０に戻る。
				if( this.r現在選択中の曲.arスコア[ this.n現在のアンカ難易度レベル ] != null )	// 曲が存在してるならここで終了。存在してないなら次のレベルへGo。
					break;
			}


			// 曲毎に表示しているスキル値を、新しい難易度レベルに合わせて取得し直す。（表示されている13曲全部。）

			C曲リストノード song = this.r現在選択中の曲;
			for( int i = 0; i < 5; i++ )
				song = this.r前の曲( song );

			for( int i = this.n現在の選択行 - 5; i < ( ( this.n現在の選択行 - 5 ) + 13 ); i++ )
			{
				int index = ( i + 13 ) % 13;
				for( int m = 0; m < 3; m++ )
				{
					this.stバー情報[ index ].nスキル値[ m ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ m ];
				}
				song = this.r次の曲( song );
			}


			// 選曲ステージに変更通知を発出し、関係Activityの対応を行ってもらう。

			CDTXMania.stage選曲.t選択曲変更通知();
		}


		/// <summary>
		/// 曲リストをリセットする
		/// </summary>
		/// <param name="cs"></param>
		public void Refresh(CSongs管理 cs, bool bRemakeSongTitleBar )		// #26070 2012.2.28 yyagi
		{
//			this.On非活性化();

			if ( cs != null && cs.list曲ルート.Count > 0 )	// 新しい曲リストを検索して、1曲以上あった
			{
				CDTXMania.Songs管理 = cs;

				if ( this.r現在選択中の曲 != null )			// r現在選択中の曲==null とは、「最初songlist.dbが無かった or 検索したが1曲もない」
				{
					this.r現在選択中の曲 = searchCurrentBreadcrumbsPosition( CDTXMania.Songs管理.list曲ルート, this.r現在選択中の曲.strBreadcrumbs );
					if ( bRemakeSongTitleBar )					// 選曲画面以外に居るときには再構成しない (非活性化しているときに実行すると例外となる)
					{
						this.t現在選択中の曲を元に曲バーを再構成する();
					}
#if false			// list子リストの中まではmatchしてくれないので、検索ロジックは手書きで実装 (searchCurrentBreadcrumbs())
					string bc = this.r現在選択中の曲.strBreadcrumbs;
					Predicate<C曲リストノード> match = delegate( C曲リストノード c )
					{
						return ( c.strBreadcrumbs.Equals( bc ) );
					};
					int nMatched = CDTXMania.Songs管理.list曲ルート.FindIndex( match );

					this.r現在選択中の曲 = ( nMatched == -1 ) ? null : CDTXMania.Songs管理.list曲ルート[ nMatched ];
					this.t現在選択中の曲を元に曲バーを再構成する();
#endif
					return;
				}
			}
			this.On非活性化();
			this.r現在選択中の曲 = null;
			this.On活性化();
		}


		/// <summary>
		/// 現在選曲している位置を検索する
		/// (曲一覧クラスを新しいものに入れ替える際に用いる)
		/// </summary>
		/// <param name="ln">検索対象のList</param>
		/// <param name="bc">検索するパンくずリスト(文字列)</param>
		/// <returns></returns>
		private C曲リストノード searchCurrentBreadcrumbsPosition( List<C曲リストノード> ln, string bc )
		{
			foreach (C曲リストノード n in ln)
			{
				if ( n.strBreadcrumbs == bc )
				{
					return n;
				}
				else if ( n.list子リスト != null && n.list子リスト.Count > 0 )	// 子リストが存在するなら、再帰で探す
				{
					C曲リストノード r = searchCurrentBreadcrumbsPosition( n.list子リスト, bc );
					if ( r != null ) return r;
				}
			}
			return null;
		}

		/// <summary>
		/// BOXのアイテム数と、今何番目を選択しているかをセットする
		/// </summary>
		public void t選択曲が変更された( bool bForce )	// #27648
		{
			C曲リストノード song = CDTXMania.stage選曲.r現在選択中の曲;
			if ( song == null )
				return;
			if ( song == song_last && bForce == false )
				return;
				
			song_last = song;
			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;
			int index = list.IndexOf( song ) + 1;
			if ( index <= 0 )
			{
				nCurrentPosition = nNumOfItems = 0;
			}
			else
			{
				nCurrentPosition = index;
				nNumOfItems = list.Count;
			}
		}

		// CActivity 実装

		public override void On活性化()
		{
			if( this.b活性化してる )
				return;

			this.e楽器パート = E楽器パート.DRUMS;
			this.b登場アニメ全部完了 = false;
			this.n目標のスクロールカウンタ = 0;
			this.n現在のスクロールカウンタ = 0;
			this.nスクロールタイマ = -1;

			// フォント作成。
			// 曲リスト文字は２倍（面積４倍）でテクスチャに描画してから縮小表示するので、フォントサイズは２倍とする。

			FontStyle regular = FontStyle.Regular;
			if( CDTXMania.ConfigIni.b選曲リストフォントを斜体にする ) regular |= FontStyle.Italic;
			if( CDTXMania.ConfigIni.b選曲リストフォントを太字にする ) regular |= FontStyle.Bold;
			this.ft曲リスト用フォント = new Font( CDTXMania.ConfigIni.str選曲リストフォント, (float) ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2 ), regular, GraphicsUnit.Pixel );
			

			// 現在選択中の曲がない（＝はじめての活性化）なら、現在選択中の曲をルートの先頭ノードに設定する。

			if( ( this.r現在選択中の曲 == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
				this.r現在選択中の曲 = CDTXMania.Songs管理.list曲ルート[ 0 ];


			// バー情報を初期化する。

			this.tバーの初期化();

			base.On活性化();

			this.t選択曲が変更された(true);		// #27648 2012.3.31 yyagi 選曲画面に入った直後の 現在位置/全アイテム数 の表示を正しく行うため
		}
		public override void On非活性化()
		{
			if( this.b活性化してない )
				return;

			CDTXMania.t安全にDisposeする( ref this.ft曲リスト用フォント );

			for( int i = 0; i < 13; i++ )
				this.ct登場アニメ用[ i ] = null;

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;

			this.tx曲名バー.Score = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar score.png" ), false );
			this.tx曲名バー.Box = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar box.png" ), false );
			this.tx曲名バー.Other = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other.png" ), false );
			this.tx選曲バー.Score = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar score selected.png" ), false );
			this.tx選曲バー.Box = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar box selected.png" ), false );
			this.tx選曲バー.Other = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other selected.png" ), false );
            this.txスキル数字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenSelect skill number on list.png"), false);
			for( int i = 0; i < 13; i++ )
				this.t曲名バーの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].col文字色 );

            this.iバー背景 = Image.FromFile(CSkin.Path(@"Graphics\5_barbase.png"), false);
            this.bバー背景L = new Bitmap(1000, 252);
            this.GraphicsBarL = Graphics.FromImage(this.bバー背景L);
            this.GraphicsBarL.DrawImage(this.iバー背景, 0, 0, 1000, 252);
            this.bバー背景R = new Bitmap(1000, 252);
            this.GraphicsBarR = Graphics.FromImage(this.bバー背景R);
            this.GraphicsBarR.DrawImage(this.iバー背景, 0, 0, 1000, 252);
            this.txバー背景L = new CTexture(CDTXMania.app.Device, this.bバー背景L, CDTXMania.TextureFormat, false);
            this.txバー背景R = new CTexture(CDTXMania.app.Device, this.bバー背景R, CDTXMania.TextureFormat, false);
            this.txパネル本体 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_image_panel.png"));
            this.txプレビュー画像 = null;
            this.txプレビュー画像がないときの画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"), false);

			int c = ( CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja" ) ? 0 : 1;
			#region [ Songs not found画像 ]
			try
			{
				using( Bitmap image = new Bitmap( 640, 128 ) )
				using( Graphics graphics = Graphics.FromImage( image ) )
				{
					string[] s1 = { "曲データが見つかりません。", "Songs not found." };
					string[] s2 = { "曲データをDTXManiaGR.exe以下の", "You need to install songs." };
					string[] s3 = { "フォルダにインストールして下さい。", "" };
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) 2f, (float) 2f );
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 0f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) 2f, (float) 44f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 42f );
					graphics.DrawString( s3[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) 2f, (float) 86f );
					graphics.DrawString( s3[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 84f );

					this.txSongNotFound = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );

					this.txSongNotFound.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );	// 半分のサイズで表示する。
				}
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "SoungNotFoundテクスチャの作成に失敗しました。" );
				this.txSongNotFound = null;
			}
			#endregion
			#region [ "曲データを検索しています"画像 ]
			try
			{
				using ( Bitmap image = new Bitmap( 1280, 200 ) )
				using ( Graphics graphics = Graphics.FromImage( image ) )
				{
					string[] s1 = { "曲データを検索しています。", "Now enumerating songs." };
					string[] s2 = { "そのまましばらくお待ち下さい。", "Please wait..." };
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) 2f, (float) 2f );
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 0f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) 2f, (float) 44f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 42f );

					this.txEnumeratingSongs = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );

					this.txEnumeratingSongs.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );	// 半分のサイズで表示する。
				}
			}
			catch ( CTextureCreateFailedException )
			{
				Trace.TraceError( "txEnumeratingSongsテクスチャの作成に失敗しました。" );
				this.txEnumeratingSongs = null;
			}
			#endregion
			#region [ 曲数表示 ]
            this.txアイテム数数字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_skill number on gauge etc.png"), false);
			#endregion
			base.OnManagedリソースの作成();
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

			CDTXMania.t安全にDisposeする( ref this.txアイテム数数字 );

			for( int i = 0; i < 13; i++ )
				CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txタイトル名 );

			CDTXMania.t安全にDisposeする( ref this.txスキル数字 );
			CDTXMania.t安全にDisposeする( ref this.txEnumeratingSongs );
			CDTXMania.t安全にDisposeする( ref this.txSongNotFound );
			CDTXMania.t安全にDisposeする( ref this.tx曲名バー.Score );
			CDTXMania.t安全にDisposeする( ref this.tx曲名バー.Box );
			CDTXMania.t安全にDisposeする( ref this.tx曲名バー.Other );
			CDTXMania.t安全にDisposeする( ref this.tx選曲バー.Score );
			CDTXMania.t安全にDisposeする( ref this.tx選曲バー.Box );
			CDTXMania.t安全にDisposeする( ref this.tx選曲バー.Other );

			base.OnManagedリソースの解放();
		}
		public override int On進行描画()
		{
			if( this.b活性化してない )
				return 0;

			#region [ 初めての進行描画 ]
			//-----------------
			if( this.b初めての進行描画 )
			{
				for( int i = 0; i < 13; i++ )
					this.ct登場アニメ用[ i ] = new CCounter( -i * 10, 100, 3, CDTXMania.Timer );

				this.nスクロールタイマ = CSound管理.rc演奏用タイマ.n現在時刻;
				CDTXMania.stage選曲.t選択曲変更通知();
				
				base.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

			
			// まだ選択中の曲が決まってなければ、曲ツリールートの最初の曲にセットする。

			if( ( this.r現在選択中の曲 == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
				this.r現在選択中の曲 = CDTXMania.Songs管理.list曲ルート[ 0 ];


			// 本ステージは、(1)登場アニメフェーズ → (2)通常フェーズ　と二段階にわけて進む。
			// ２つしかフェーズがないので CStage.eフェーズID を使ってないところがまた本末転倒。

			
			// 進行。

			if( !this.b登場アニメ全部完了 )
			{
				#region [ (1) 登場アニメフェーズの進行。]
				//-----------------
				for( int i = 0; i < 13; i++ )	// パネルは全13枚。
				{
					this.ct登場アニメ用[ i ].t進行();

					if( this.ct登場アニメ用[ i ].b終了値に達した )
						this.ct登場アニメ用[ i ].t停止();
				}

				// 全部の進行が終わったら、this.b登場アニメ全部完了 を true にする。

				this.b登場アニメ全部完了 = true;
				for( int i = 0; i < 13; i++ )	// パネルは全13枚。
				{
					if( this.ct登場アニメ用[ i ].b進行中 )
					{
						this.b登場アニメ全部完了 = false;	// まだ進行中のアニメがあるなら false のまま。
						break;
					}
				}


				//-----------------
				#endregion
			}
			else
			{
				#region [ (2) 通常フェーズの進行。]
				//-----------------
				long n現在時刻 = CDTXMania.Timer.n現在時刻;
				
				if( n現在時刻 < this.nスクロールタイマ )	// 念のため
					this.nスクロールタイマ = n現在時刻;

				const int nアニメ間隔 = 2;
				while( ( n現在時刻 - this.nスクロールタイマ ) >= nアニメ間隔 )
				{
					int n加速度 = 1;
					int n残距離 = Math.Abs( (int) ( this.n目標のスクロールカウンタ - this.n現在のスクロールカウンタ ) );

					#region [ 残距離が遠いほどスクロールを速くする（＝n加速度を多くする）。]
					//-----------------
					if( n残距離 <= 100 )
					{
						n加速度 = 2;
					}
					else if( n残距離 <= 300 )
					{
						n加速度 = 3;
					}
					else if( n残距離 <= 500 )
					{
						n加速度 = 4;
					}
					else
					{
						n加速度 = 8;
					}
					//-----------------
					#endregion

					#region [ 加速度を加算し、現在のスクロールカウンタを目標のスクロールカウンタまで近づける。 ]
					//-----------------
					if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )		// (A) 正の方向に未達の場合：
					{
						this.n現在のスクロールカウンタ += n加速度;								// カウンタを正方向に移動する。

						if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
							this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;	// 到着！スクロール停止！
					}

					else if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )	// (B) 負の方向に未達の場合：
					{
						this.n現在のスクロールカウンタ -= n加速度;								// カウンタを負方向に移動する。

						if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )	// 到着！スクロール停止！
							this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
					//-----------------
					#endregion

					if( this.n現在のスクロールカウンタ >= 100 )		// １行＝100カウント。
					{
						#region [ パネルを１行上にシフトする。]
						//-----------------

						// 選択曲と選択行を１つ下の行に移動。

						this.r現在選択中の曲 = this.r次の曲( this.r現在選択中の曲 );
						this.n現在の選択行 = ( this.n現在の選択行 + 1 ) % 13;


						// 選択曲から７つ下のパネル（＝新しく最下部に表示されるパネル。消えてしまう一番上のパネルを再利用する）に、新しい曲の情報を記載する。

						C曲リストノード song = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song = this.r次の曲( song );

						int index = ( this.n現在の選択行 + 7 ) % 13;	// 新しく最下部に表示されるパネルのインデックス（0～12）。
						this.stバー情報[ index ].strタイトル文字列 = song.strタイトル;
						this.stバー情報[ index ].col文字色 = song.col文字色;
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );


						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 5; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 13; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 5 ) + i ) + 13 ) % 13;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
							song2 = this.r次の曲( song2 );
						}

						
						// 新しく最下部に表示されるパネル用のスキル値を取得。

						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];


						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ -= 100;
						this.n目標のスクロールカウンタ -= 100;

						this.t選択曲が変更された(false);				// スクロールバー用に今何番目を選択しているかを更新

						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲.t選択曲変更通知();		// スクロール完了＝選択曲変更！

						//-----------------
						#endregion
					}
					else if( this.n現在のスクロールカウンタ <= -100 )
					{
						#region [ パネルを１行下にシフトする。]
						//-----------------

						// 選択曲と選択行を１つ上の行に移動。

						this.r現在選択中の曲 = this.r前の曲( this.r現在選択中の曲 );
						this.n現在の選択行 = ( ( this.n現在の選択行 - 1 ) + 13 ) % 13;


						// 選択曲から５つ上のパネル（＝新しく最上部に表示されるパネル。消えてしまう一番下のパネルを再利用する）に、新しい曲の情報を記載する。

						C曲リストノード song = this.r現在選択中の曲;
						for( int i = 0; i < 5; i++ )
							song = this.r前の曲( song );

						int index = ( ( this.n現在の選択行 - 5 ) + 13 ) % 13;	// 新しく最上部に表示されるパネルのインデックス（0～12）。
						this.stバー情報[ index ].strタイトル文字列 = song.strタイトル;
						this.stバー情報[ index ].col文字色 = song.col文字色;
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );


						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 5; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 13; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 5 ) + i ) + 13 ) % 13;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
							song2 = this.r次の曲( song2 );
						}

		
						// 新しく最上部に表示されるパネル用のスキル値を取得。
						
						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];


						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ += 100;
						this.n目標のスクロールカウンタ += 100;

						this.t選択曲が変更された(false);				// スクロールバー用に今何番目を選択しているかを更新
						
						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲.t選択曲変更通知();		// スクロール完了＝選択曲変更！
						//-----------------
						#endregion
					}

					this.nスクロールタイマ += nアニメ間隔;
				}
				//-----------------
				#endregion
			}


			// 描画。

			if( this.r現在選択中の曲 == null )
			{
				#region [ 曲が１つもないなら「Songs not found.」を表示してここで帰れ。]
				//-----------------
				if ( bIsEnumeratingSongs )
				{
					if ( this.txEnumeratingSongs != null )
					{
						this.txEnumeratingSongs.t2D描画( CDTXMania.app.Device, 320, 160 );
					}
				}
				else
				{
					if ( this.txSongNotFound != null )
						this.txSongNotFound.t2D描画( CDTXMania.app.Device, 640, 400 );
				}
				//-----------------
				#endregion

				return 0;
			}
            SlimDX.Matrix mat = SlimDX.Matrix.Identity;
            mat *= SlimDX.Matrix.Translation(-600, 18, 0);
            mat *= SlimDX.Matrix.Scaling(1.5f, 1.26f, 1.0f);
            mat *= SlimDX.Matrix.RotationY(0.4f);
            SlimDX.Matrix mat2 = SlimDX.Matrix.Identity;
            mat2 *= SlimDX.Matrix.Translation(600, 18, 0);
            mat2 *= SlimDX.Matrix.Scaling(1.5f, 1.26f, 1.0f);
            mat2 *= SlimDX.Matrix.RotationY(-0.4f);
            this.txバー背景L.t3D描画(CDTXMania.app.Device, mat);
            this.txバー背景R.t3D描画(CDTXMania.app.Device, mat2);
            this.txパネル本体.t2D描画(CDTXMania.app.Device, 457, 164);
				#region [ 通常フェーズの描画。]
				//-----------------
				for( int i = 0; i < 13; i++ )	// パネルは全13枚。
				{
					if( ( i == 0 && this.n現在のスクロールカウンタ > 0 ) ||		// 最上行は、上に移動中なら表示しない。
						( i == 12 && this.n現在のスクロールカウンタ < 0 ) )		// 最下行は、下に移動中なら表示しない。
						continue;

					int nパネル番号 = ( ( ( this.n現在の選択行 - 5 ) + i ) + 13 ) % 13;
					int n見た目の行番号 = i;
					int n次のパネル番号 = ( this.n現在のスクロールカウンタ <= 0 ) ? ( ( i + 1 ) % 13 ) : ( ( ( i - 1 ) + 13 ) % 13 );
					int x = this.ptバーの基本座標[ n見た目の行番号 ].X + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].X - this.ptバーの基本座標[ n見た目の行番号 ].X ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
					int y = this.ptバーの基本座標[ n見た目の行番号 ].Y + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].Y - this.ptバーの基本座標[ n見た目の行番号 ].Y ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
		
					if( ( i == 5 ) && ( this.n現在のスクロールカウンタ == 0 ) )
					{
						// (A) スクロールが停止しているときの選択曲バーの描画。

						#region [ バーテクスチャを描画。]
						//-----------------
						//this.tバーの描画( 410, 270, this.stバー情報[ nパネル番号 ].eバー種別, true );
						//-----------------
						#endregion
                        #region [ ジャケット画像を描画 ]
                        //-----------------
                        this.t選択中の曲でプレビュー画像の指定があれば構築する();
                        this.t描画処理・プレビュー画像();
                        //-----------------
                        #endregion
                        #region [ タイトル名テクスチャを描画。]
                        //-----------------
						if( this.stバー情報[ nパネル番号 ].txタイトル名 != null )
							this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, 554, 210 );
						//-----------------
						#endregion
						#region [ スキル値を描画。]
						//-----------------
						if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score ) && ( this.e楽器パート != E楽器パート.UNKNOWN ) )
							this.tスキル値の描画( 490, 312, this.stバー情報[ nパネル番号 ].nスキル値[ (int) this.e楽器パート ] );
						//-----------------
						#endregion
                    }
					else
					{
                        
						// (B) スクロール中の選択曲バー、またはその他のバーの描画。

						#region [ バーテクスチャを描画。]
						//-----------------
						this.tバーの描画( x, y, this.stバー情報[ nパネル番号 ].eバー種別, false );
						//-----------------
						#endregion
                        #region [ ジャケット画像を描画 ]
                        //-----------------
                        this.t選択中の曲以外でプレビュー画像の指定があれば構築する(i);
                        this.t描画処理・選択曲以外のプレビュー画像(n見た目の行番号);
                        //-----------------
                        #endregion
                        #region [ タイトル名テクスチャを描画。]
                        //-----------------
						if( this.stバー情報[ nパネル番号 ].txタイトル名 != null )
							this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, x + 0x58, y + 6 );
						//-----------------
						#endregion
						#region [ スキル値を描画。]
						//-----------------
						if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score ) && ( this.e楽器パート != E楽器パート.UNKNOWN ) )
							this.tスキル値の描画( x + 34, y + 18, this.stバー情報[ nパネル番号 ].nスキル値[ (int) this.e楽器パート ] );
						//-----------------
						#endregion
                        
					}
				//-----------------
				#endregion
			}
			#region [ スクロール地点の計算(描画はCActSelectShowCurrentPositionにて行う) #27648 ]
			int py;
			double d = 0;
			if ( nNumOfItems > 1 )
			{
				d = ( 336 - 6 - 8 ) / (double) ( nNumOfItems - 1 );
				py = (int) ( d * ( nCurrentPosition - 1 ) );
			}
			else
			{
				d = 0;
				py = 0;
			}
			int delta = (int) ( d * this.n現在のスクロールカウンタ / 100 );
			if ( py + delta <= 336 - 6 - 8 )
			{
				this.nスクロールバー相対y座標 = py + delta;
			}
			#endregion

			#region [ アイテム数の描画 #27648 ]
			tアイテム数の描画();
			#endregion
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        private enum Eバー種別 { Score, Box, Other }

		private struct STバー
		{
			public CTexture Score;
			public CTexture Box;
			public CTexture Other;
			public CTexture this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Score;

						case 1:
							return this.Box;

						case 2:
							return this.Other;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Score = value;
							return;

						case 1:
							this.Box = value;
							return;

						case 2:
							this.Other = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

        private struct STバー情報
        {
            public CActSelect曲リスト.Eバー種別 eバー種別;
            public Cスコア cスコア;
            public string strタイトル文字列;
            public CTexture txタイトル名;
            public CTexture txジャケット;
            public CTexture tx選択曲以外のタイトル名L;
            public CTexture tx選択曲以外のタイトル名R;
            public STDGBVALUE<int> nスキル値;
            public Color col文字色;
        }

		private struct ST選曲バー
		{
			public CTexture Score;
			public CTexture Box;
			public CTexture Other;
			public CTexture this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Score;

						case 1:
							return this.Box;

						case 2:
							return this.Other;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Score = value;
							return;

						case 1:
							this.Box = value;
							return;

						case 2:
							this.Other = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		private bool b登場アニメ全部完了;
		private Color color文字影 = Color.FromArgb( 0x40, 10, 10, 10 );
		private CCounter[] ct登場アニメ用 = new CCounter[ 13 ];
		private E楽器パート e楽器パート;
		private Font ft曲リスト用フォント;
		private long nスクロールタイマ;
		private int n現在のスクロールカウンタ;
		private int n現在の選択行;
		private int n目標のスクロールカウンタ;
        private readonly Point[] ptバーの基本座標 = new Point[] { new Point(0x2c4, 30), new Point(0x272, 0x51), new Point(0x242, 0x84), new Point(0x222, 0xb7), new Point(0x210, 0xea), new Point(0x1d0, 0x127), new Point(0x224, 0x183), new Point(0x242, 0x1b6), new Point(0x270, 0x1e9), new Point(0x2ae, 540), new Point(0x314, 0x24f), new Point(0x3e4, 0x282), new Point(0x500, 0x2b5) };
		private STバー情報[] stバー情報 = new STバー情報[ 13 ];
		private CTexture txSongNotFound, txEnumeratingSongs;
		private CTexture txスキル数字;
		private CTexture txアイテム数数字;
        private CTexture txパネル本体;

        //このへんは後から構造体にするかどうにかする。
        //兎にも角にも
        protected struct ST中心点
        {
            public float x;
            public float y;
            public float z;
            public float rotY;
        }
        protected readonly ST中心点[] stマトリックス座標 = new ST中心点[ 13 ] {
			#region [ 実は円弧配置になってない。射影行列間違ってるよスターレインボウ見せる気かよ… ]
			//-----------------
             new ST中心点() { x = -533.8936f, y = 210f, z = -289.5575f, rotY = -0.9279888f },
             new ST中心点() { x = -533.8936f, y = 210f, z = -289.5575f, rotY = -0.9279888f },
			 new ST中心点() { x = -533.8936f, y = 210f, z = -289.5575f, rotY = -0.9279888f },
			 new ST中心点() { x = -423.8936f, y = 210f, z = -169.5575f, rotY = -0.6579891f },
	    	 new ST中心点() { x = -297.5025f, y = 210f, z = -74.37564f, rotY = -0.4808382f },
		     new ST中心点() { x = -153.9001f, y = 210f, z = -20.52002f, rotY = -0.2605f },
			 new ST中心点() { x = 0.00002622683f, y = 210f, z = 0f, rotY = 0f }, 
			 new ST中心点() { x = 153.9002f, y = 210f, z = -20.52002f, rotY = 0.2605f },
			 new ST中心点() { x = 297.5025f, y = 210f, z = -74.37564f, rotY = 0.4808382f },
			 new ST中心点() { x = 423.8936f, y = 210f, z = -169.5575f, rotY = 0.6579891f },
			 new ST中心点() { x = 533.8936f, y = 210f, z = -289.5575f, rotY = 0.9279888f },
             new ST中心点() { x = 533.8936f, y = 210f, z = -289.5575f, rotY = 0.9279888f },
             new ST中心点() { x = 533.8936f, y = 210f, z = -289.5575f, rotY = 0.9279888f }
            };
			//-----------------
			#endregion

        private string str現在のファイル名;
        private CTexture txプレビュー画像;
        private CTexture r表示するプレビュー画像;
        private CTexture txプレビュー画像がないときの画像;

        private CTexture txバー背景L;
        private CTexture txバー背景R;
        private Graphics GraphicsBarL;
        private Graphics GraphicsBarR;
        private Bitmap bバー背景L;
        private Bitmap bバー背景R;
        private Image iバー背景;

		private STバー tx曲名バー;
		private ST選曲バー tx選曲バー;

		private int nCurrentPosition = 0;
		private int nNumOfItems = 0;

		//private string strBoxDefSkinPath = "";
		private Eバー種別 e曲のバー種別を返す( C曲リストノード song )
		{
			if( song != null )
			{
				switch( song.eノード種別 )
				{
					case C曲リストノード.Eノード種別.SCORE:
					case C曲リストノード.Eノード種別.SCORE_MIDI:
						return Eバー種別.Score;

					case C曲リストノード.Eノード種別.BOX:
					case C曲リストノード.Eノード種別.BACKBOX:
						return Eバー種別.Box;
				}
			}
			return Eバー種別.Other;
		}
		private C曲リストノード r次の曲( C曲リストノード song )
		{
			if( song == null )
				return null;

			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;
	
			int index = list.IndexOf( song );

			if( index < 0 )
				return null;

			if( index == ( list.Count - 1 ) )
				return list[ 0 ];

			return list[ index + 1 ];
		}
		private C曲リストノード r前の曲( C曲リストノード song )
		{
			if( song == null )
				return null;

			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;

			int index = list.IndexOf( song );
	
			if( index < 0 )
				return null;

			if( index == 0 )
				return list[ list.Count - 1 ];

			return list[ index - 1 ];
		}
		private void tスキル値の描画( int x, int y, int nスキル値 )
		{
			if( nスキル値 <= 0 || nスキル値 > 100 )		// スキル値 0 ＝ 未プレイ なので表示しない。
				return;

			int color = ( nスキル値 == 100 ) ? 3 : ( nスキル値 / 25 );

			int n百の位 = nスキル値 / 100;
			int n十の位 = ( nスキル値 % 100 ) / 10;
			int n一の位 = ( nスキル値 % 100 ) % 10;


			// 百の位の描画。

			if( n百の位 > 0 )
				this.tスキル値の描画・１桁描画( x, y, n百の位, color );


			// 十の位の描画。

			if( n百の位 != 0 || n十の位 != 0 )
				this.tスキル値の描画・１桁描画( x + 14, y, n十の位, color );


			// 一の位の描画。

			this.tスキル値の描画・１桁描画( x + 0x1c, y, n一の位, color );
		}
		private void tスキル値の描画・１桁描画( int x, int y, int n数値, int color )
		{
			int dx = ( n数値 % 5 ) * 9;
			int dy = ( n数値 / 5 ) * 12;
			
			switch( color )
			{
				case 0:
					if( this.txスキル数字 != null )
						this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 45 + dx, 24 + dy, 9, 12 ) );
					break;

				case 1:
					if( this.txスキル数字 != null )
						this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 45 + dx, dy, 9, 12 ) );
					break;

				case 2:
					if( this.txスキル数字 != null )
						this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, 24 + dy, 9, 12 ) );
					break;

				case 3:
					if( this.txスキル数字 != null )
						this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, dy, 9, 12 ) );
					break;
			}
		}
		private void tバーの初期化()
		{
			C曲リストノード song = this.r現在選択中の曲;
			
			if( song == null )
				return;

			for( int i = 0; i < 5; i++ )
				song = this.r前の曲( song );

			for( int i = 0; i < 13; i++ )
			{
				this.stバー情報[ i ].strタイトル文字列 = song.strタイトル;
				this.stバー情報[ i ].col文字色 = song.col文字色;
				this.stバー情報[ i ].eバー種別 = this.e曲のバー種別を返す( song );
				
				for( int j = 0; j < 3; j++ )
					this.stバー情報[ i ].nスキル値[ j ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ j ];

				song = this.r次の曲( song );
			}

			this.n現在の選択行 = 5;
		}
		private void tバーの描画( int x, int y, Eバー種別 type, bool b選択曲 )
		{
			if( x >= SampleFramework.GameWindowSize.Width || y >= SampleFramework.GameWindowSize.Height )
				return;

                if (b選択曲)
                {
                    #region [ (A) 選択曲の場合 ]
                    //-----------------
                    if (this.tx選曲バー[(int)type] != null)
                        this.tx選曲バー[(int)type].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, 0, 128, 96));	// ヘサキ
                    x += 128;

                    var rc = new Rectangle(128, 0, 128, 96);
                    while (x < 1280)
                    {
                        if (this.tx選曲バー[(int)type] != null)
                            this.tx選曲バー[(int)type].t2D描画(CDTXMania.app.Device, x, y, rc);	// 胴体；64pxずつ横につなげていく。
                        x += 128;
                    }
                    //-----------------
                    #endregion
                }
                else
                {
                    #region [ (B) その他の場合 ]
                    //-----------------
                    if (this.tx曲名バー[(int)type] != null)
                        this.tx曲名バー[(int)type].t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, 0, 128, 48));		// ヘサキ
                    x += 128;

                    var rc = new Rectangle(0, 48, 128, 48);
                    while (x < 1280)
                    {
                        //if (this.tx曲名バー[(int)type] != null)
                            //this.tx曲名バー[(int)type].t2D描画(CDTXMania.app.Device, x, y, rc);	// 胴体；64pxずつ横につなげていく。
                        x += 128;
                    }
                    //-----------------
                    #endregion
                }
        }
		private void t曲名バーの生成( int nバー番号, string str曲名, Color color )
		{
			if( nバー番号 < 0 || nバー番号 > 12 )
				return;

			try
			{
				SizeF sz曲名;

				#region [ 曲名表示に必要となるサイズを取得する。]
				//-----------------
				using( var bmpDummy = new Bitmap( 1, 1 ) )
				{
					var g = Graphics.FromImage( bmpDummy );
					g.PageUnit = GraphicsUnit.Pixel;
					sz曲名 = g.MeasureString( str曲名, this.ft曲リスト用フォント );
				}
				//-----------------
				#endregion

				int n最大幅px = 0x310;
				int height = 0x25;
				int width = (int) ( ( sz曲名.Width + 2 ) * 0.5f );
				if( width > ( CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2 ) )
					width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

				float f拡大率X = ( width <= n最大幅px ) ? 0.5f : ( ( (float) n最大幅px / (float) width ) * 0.5f );	// 長い文字列は横方向に圧縮。

				using( var bmp = new Bitmap( width * 2, height * 2, PixelFormat.Format32bppArgb ) )		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
				using( var g = Graphics.FromImage( bmp ) )
				{
					g.TextRenderingHint = TextRenderingHint.AntiAlias;
					float y = ( ( ( float ) bmp.Height ) / 2f ) - ( ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2f ) / 2f );
					g.DrawString( str曲名, this.ft曲リスト用フォント, new SolidBrush( this.color文字影 ), (float) 2f, (float) ( y + 2f ) );
					g.DrawString( str曲名, this.ft曲リスト用フォント, new SolidBrush( color ), 0f, y );

					CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txタイトル名 );

					this.stバー情報[ nバー番号 ].txタイトル名 = new CTexture( CDTXMania.app.Device, bmp, CDTXMania.TextureFormat );
					this.stバー情報[ nバー番号 ].txタイトル名.vc拡大縮小倍率 = new Vector3( f拡大率X, 0.5f, 1f );
				}
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", str曲名 );
				this.stバー情報[ nバー番号 ].txタイトル名 = null;
			}
		}

        private void t選択曲以外の曲名バーの生成L(int nバー番号, string str曲名, Color color)
        {
            if (nバー番号 < 0 || nバー番号 > 12)
                return;

            try
            {
                SizeF sz曲名;

                #region [ 曲名表示に必要となるサイズを取得する。]
                //-----------------
                using (var bmpDummy = new Bitmap(1, 1))
                {
                    var g = Graphics.FromImage(bmpDummy);
                    g.PageUnit = GraphicsUnit.Pixel;
                    sz曲名 = g.MeasureString(str曲名, this.ft曲リスト用フォント);
                }
                //-----------------
                #endregion

                int n最大幅px = 392;
                int height = 25;
                int width = (int)((sz曲名.Width + 2) * 0.5f);
                if (width > (CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2))
                    width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

                float f拡大率X = (width <= n最大幅px) ? 0.5f : (((float)n最大幅px / (float)width) * 0.5f);	// 長い文字列は横方向に圧縮。

                using (var bmp = new Bitmap(width * 2, height * 2, PixelFormat.Format32bppArgb))		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
                using (var g = Graphics.FromImage(bmp))
                {
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    float y = (((float)bmp.Height) / 2f) - ((CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2f) / 2f);
                    g.DrawString(str曲名, this.ft曲リスト用フォント, new SolidBrush(this.color文字影), (float)2f, (float)(y + 2f));
                    g.DrawString(str曲名, this.ft曲リスト用フォント, new SolidBrush(color), 0f, y);

                    CDTXMania.t安全にDisposeする(ref this.stバー情報[nバー番号].tx選択曲以外のタイトル名L);

                    this.stバー情報[nバー番号].tx選択曲以外のタイトル名L = new CTexture(CDTXMania.app.Device, bmp, CDTXMania.TextureFormat);
                    this.stバー情報[nバー番号].tx選択曲以外のタイトル名L.vc拡大縮小倍率 = new Vector3(f拡大率X, 0.5f, 1f);
                }
            }
            catch (CTextureCreateFailedException)
            {
                Trace.TraceError("曲名テクスチャの作成に失敗しました。[{0}]", str曲名);
                this.stバー情報[nバー番号].tx選択曲以外のタイトル名L = null;
            }
        }
        private void t選択曲以外の曲名バーの生成R(int nバー番号, string str曲名, Color color)
        {
            if (nバー番号 < 0 || nバー番号 > 12)
                return;

            try
            {
                SizeF sz曲名;

                #region [ 曲名表示に必要となるサイズを取得する。]
                //-----------------
                using (var bmpDummy = new Bitmap(1, 1))
                {
                    var g = Graphics.FromImage(bmpDummy);
                    g.PageUnit = GraphicsUnit.Pixel;
                    sz曲名 = g.MeasureString(str曲名, this.ft曲リスト用フォント);
                }
                //-----------------
                #endregion

                int n最大幅px = 392;
                int height = 25;
                int width = (int)((sz曲名.Width + 2) * 0.5f);
                if (width > (CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2))
                    width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

                float f拡大率X = (width <= n最大幅px) ? 0.5f : (((float)n最大幅px / (float)width) * 0.5f);	// 長い文字列は横方向に圧縮。

                using (var bmp = new Bitmap(width * 2, height * 2, PixelFormat.Format32bppArgb))		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
                using (var g = Graphics.FromImage(bmp))
                {
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    float y = (((float)bmp.Height) / 2f) - ((CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2f) / 2f);
                    g.DrawString(str曲名, this.ft曲リスト用フォント, new SolidBrush(this.color文字影), (float)2f, (float)(y + 2f));
                    g.DrawString(str曲名, this.ft曲リスト用フォント, new SolidBrush(color), 0f, y);

                    CDTXMania.t安全にDisposeする(ref this.stバー情報[nバー番号].tx選択曲以外のタイトル名R);

                    this.stバー情報[nバー番号].tx選択曲以外のタイトル名R = new CTexture(CDTXMania.app.Device, bmp, CDTXMania.TextureFormat);
                    this.stバー情報[nバー番号].tx選択曲以外のタイトル名R.vc拡大縮小倍率 = new Vector3(f拡大率X, 0.5f, 1f);
                }
            }
            catch (CTextureCreateFailedException)
            {
                Trace.TraceError("曲名テクスチャの作成に失敗しました。[{0}]", str曲名);
                this.stバー情報[nバー番号].tx選択曲以外のタイトル名R = null;
            }
        }

		private void tアイテム数の描画()
		{
			string s = nCurrentPosition.ToString() + "/" + nNumOfItems.ToString();
			int x = 1820 - 8 - 12;
			int y = 500;

			for ( int p = s.Length - 1; p >= 0; p-- )
			{
				tアイテム数の描画・１桁描画( x, y, s[ p ] );
				x -= 8;
			}
		}
		private void tアイテム数の描画・１桁描画( int x, int y, char s数値 )
		{
			int dx, dy;
			if ( s数値 == '/' )
			{
				dx = 48;
				dy = 0;
			}
			else
			{
				int n = (int) s数値 - (int) '0';
				dx = ( n % 6 ) * 16;
				dy = ( n / 6 ) * 16;
			}
			if ( this.txアイテム数数字 != null )
			{
				this.txアイテム数数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, dy, 16, 16 ) );
			}
		}
        private bool t選択中の曲でプレビュー画像の指定があれば構築する()
        {
            Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
            if ((cスコア == null) || string.IsNullOrEmpty(cスコア.譜面情報.Preimage))
            {
                return false;
            }
            string str = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Preimage;
            if (!str.Equals(this.str現在のファイル名))
            {
                CDTXMania.tテクスチャの解放(ref this.txプレビュー画像);
                this.str現在のファイル名 = str;
                if (!File.Exists(this.str現在のファイル名))
                {
                    Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { this.str現在のファイル名 });
                    return false;
                }
                this.txプレビュー画像 = CDTXMania.tテクスチャの生成(this.str現在のファイル名, false);
                if (this.txプレビュー画像 != null)
                {
                    this.r表示するプレビュー画像 = this.txプレビュー画像;
                }
                else
                {
                    this.txプレビュー画像 = this.txプレビュー画像がないときの画像;
                }
            }
            return true;
        }
        private bool t選択中の曲以外でプレビュー画像の指定があれば構築する(int n曲番号)
        {
            Cスコア cスコア = this.stバー情報[ n曲番号 ].cスコア;
            if ((cスコア == null) || string.IsNullOrEmpty(cスコア.譜面情報.Preimage))
            {
                return false;
            }
            string str = cスコア.ファイル情報.フォルダの絶対パス + cスコア.譜面情報.Preimage;
            if (!str.Equals(this.str現在のファイル名))
            {
                CDTXMania.tテクスチャの解放(ref this.txプレビュー画像);
                this.str現在のファイル名 = str;
                if (!File.Exists(this.str現在のファイル名))
                {
                    Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { this.str現在のファイル名 });
                    return false;
                }
                this.stバー情報[n曲番号].txジャケット = CDTXMania.tテクスチャの生成(this.str現在のファイル名, false);
                if (this.stバー情報[n曲番号].txジャケット != null)
                {
                    this.r表示するプレビュー画像 = this.stバー情報[n曲番号].txジャケット;
                }
                else
                {
                    this.r表示するプレビュー画像 = this.txプレビュー画像がないときの画像;
                }
            }
            return true;
        }
        protected CTexture tサムネイルテクスチャを作成する(string strフォルダパス, Cスコア cスコア)
        {
            string files = strフォルダパス + cスコア.譜面情報.Preimage;
            foreach (var file in files)
            {
                //string ext = Path.GetExtension(file).ToLower();

                //using (var bmpFromfile = new Bitmap(file))
                using (var bmp = new Bitmap(400, 400))
                using (var g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);

                    #region [ bmpFromFile を、bmp 内部に収まり、センタリングされる位置に描画する。小さい画像は拡大し、大きい画像は縮小する。]
                    /*
                        //-----------------
                        int w = bmpFromfile.Width;
                        int h = bmpFromfile.Height;

                        if (w > h)
                        {
                            h = (int)(h * (double)(bmp.Width - n縁の幅px * 2) / (double)w);
                            w = bmp.Width - n縁の幅px * 2;
                        }
                        else
                        {
                            w = (int)(w * (double)(bmp.Height - n縁の幅px * 2) / (double)h);
                            h = bmp.Height - n縁の幅px * 2;
                        }
                        int x = (bmp.Width - w) / 2;
                        int y = (bmp.Height - h) / 2;

                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                        g.DrawImage(bmpFromfile, new Rectangle(x, y, w, h));
                        //-----------------
                        */
                    #endregion

                    return new CTexture(CDTXMania.app.Device, bmp, CDTXMania.TextureFormat, false);
                }
            }
            return CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"));
        }
        private unsafe void t描画処理・プレビュー画像()
		{
                int x = 0x24;
                int y = 0x18;
				if( this.r表示するプレビュー画像 != null )
				{
					int width = this.r表示するプレビュー画像.sz画像サイズ.Width;
					int height = this.r表示するプレビュー画像.sz画像サイズ.Height;
					if( width > 400 )
					{
						width = 400;
					}
					if( height > 400 )
					{
						height = 400;
					}
					this.r表示するプレビュー画像.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, width, height ) );
				}
		}
        private unsafe void t描画処理・選択曲以外のプレビュー画像(int n位置番号)
        {
            int x = n位置番号 * 50;
            int y = 200;

            SlimDX.Matrix r = SlimDX.Matrix.Identity;
            r *= SlimDX.Matrix.Translation(this.stマトリックス座標[n位置番号].x, 0f, 0f);
            r *= SlimDX.Matrix.RotationY(this.stマトリックス座標[n位置番号].rotY);
            if (this.r表示するプレビュー画像 != null)
            {
                int width = this.r表示するプレビュー画像.sz画像サイズ.Width;
                int height = this.r表示するプレビュー画像.sz画像サイズ.Height;

                //this.r表示するプレビュー画像.t2D描画(CDTXMania.app.Device, x, y);
                this.r表示するプレビュー画像.t3D描画(CDTXMania.app.Device, r);
            }
        }
        //-----------------
		#endregion
	}
}
