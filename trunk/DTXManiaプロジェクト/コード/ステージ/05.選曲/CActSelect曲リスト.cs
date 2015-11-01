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
    //ここをXG風にする際に使ったコードはSSTから拝借、改造している。
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


            this.stパネルマップ = null;
            this.stパネルマップ = new STATUSPANEL[12];		// yyagi: 以下、手抜きの初期化でスマン
            string[] labels = new string[12] {
            "DTXMANIA",     //0
            "DEBUT",        //1
            "NOVICE",       //2
            "REGULAR",      //3
            "EXPERT",       //4
            "MASTER",       //5
            "BASIC",        //6
            "ADVANCED",     //7
            "EXTREME",      //8
            "RAW",          //9
            "RWS",          //10
            "REAL"          //11
            };
            int[] status = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            for (int i = 0; i < 12; i++)
            {
                this.stパネルマップ[i] = default(STATUSPANEL);
                this.stパネルマップ[i].status = status[i];
                this.stパネルマップ[i].label = labels[i];
            }
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
		/// <summary>
		/// 主にCSong管理.cs内にあるソート機能を、delegateで呼び出す。
		/// </summary>
		/// <param name="sf">ソート用に呼び出すメソッド</param>
		/// <param name="eInst">ソート基準とする楽器</param>
		/// <param name="order">-1=降順, 1=昇順</param>
		public void t曲リストのソート( DGSortFunc sf, E楽器パート eInst, int order, params object[] p )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( this.r現在選択中の曲 );
			if ( songList == null )
			{
				// 何もしない;
			}
			else
			{
//				CDTXMania.Songs管理.t曲リストのソート3_演奏回数の多い順( songList, eInst, order );
				sf( songList, eInst, order, p );
//				this.r現在選択中の曲 = CDTXMania
				this.t現在選択中の曲を元に曲バーを再構成する();
			}
		}

		public bool tBOXに入る()
		{
//Trace.TraceInformation( "box enter" );
//Trace.TraceInformation( "Skin現在Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin現在System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "Skin現pt: " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin指定: " + CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) );
//Trace.TraceInformation( "Skinpath: " + this.r現在選択中の曲.strSkinPath );
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
				// BOXに入るときは、スキン変更発生時のみboxdefスキン設定の更新を行う
				CDTXMania.Skin.SetCurrentSkinSubfolderFullName(
					CDTXMania.Skin.GetSkinSubfolderFullNameFromSkinName( CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) ), false );
			}

//Trace.TraceInformation( "Skin変更: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "Skin変更Current : "+  CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin変更System  : "+  CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin変更BoxDef  : "+  CSkin.strBoxDefSkinSubfolderFullName );
            if( this.tx選択されている曲の曲名 != null )
            {   
                this.tx選択されている曲の曲名.Dispose();
                this.tx選択されている曲の曲名 = null;
            }
            if( this.tx選択されている曲のアーティスト名 != null )
            {
                this.tx選択されている曲のアーティスト名.Dispose();
                this.tx選択されている曲のアーティスト名 = null;
            }
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
//Trace.TraceInformation( "box exit" );
//Trace.TraceInformation( "Skin現在Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin現在System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "Skin現pt: " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin指定: " + CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) );
//Trace.TraceInformation( "Skinpath: " + this.r現在選択中の曲.strSkinPath );
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
			}
			// スキン変更が発生しなくても、boxdef圏外に出る場合は、boxdefスキン設定の更新が必要
			// (ユーザーがboxdefスキンをConfig指定している場合への対応のために必要)
			// tBoxに入る()とは処理が微妙に異なるので注意
			CDTXMania.Skin.SetCurrentSkinSubfolderFullName(
				( this.r現在選択中の曲.strSkinPath == "" ) ? "" : CDTXMania.Skin.GetSkinSubfolderFullNameFromSkinName( CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) ), false );
//Trace.TraceInformation( "SKIN変更: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "SKIN変更Current : "+  CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "SKIN変更System  : "+  CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "SKIN変更BoxDef  : "+  CSkin.strBoxDefSkinSubfolderFullName );
            if( this.tx選択されている曲の曲名 != null )
            {   
                this.tx選択されている曲の曲名.Dispose();
                this.tx選択されている曲の曲名 = null;
            }
            if( this.tx選択されている曲のアーティスト名 != null )
            {
                this.tx選択されている曲のアーティスト名.Dispose();
                this.tx選択されている曲のアーティスト名 = null;
            }
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
                this.tアーティスト名テクスチャの生成( i, this.stバー情報[ i ].strアーティスト名 );
                this.tパネルの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].strアーティスト名, this.stバー情報[ i ].col文字色 );
                if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
			    {
    			    //txTumbnail = this.tサムネイルテクスチャを作成する( Path.GetDirectoryName( song.ScoreFile ) );
                    this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
				    this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
				}
                txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
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

            this.tラベル名からステータスパネルを決定する( this.r現在選択中の曲.ar難易度ラベル[ this.n現在選択中の曲の現在の難易度レベル ] );

            switch( this.nIndex  )
            {
                case 2:
                    CDTXMania.Skin.soundNovice.t再生する();
                    string strnov = CSkin.Path( @"Sounds\Novice.ogg" );
                    if( !File.Exists( strnov ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                case 3:
                    CDTXMania.Skin.soundRegular.t再生する();
                    string strreg = CSkin.Path( @"Sounds\Regular.ogg" );
                    if( !File.Exists( strreg ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                case 4:
                    CDTXMania.Skin.soundExpert.t再生する();
                    string strexp = CSkin.Path( @"Sounds\Expert.ogg" );
                    if( !File.Exists( strexp ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                case 5:
                    CDTXMania.Skin.soundMaster.t再生する();
                    string strmas = CSkin.Path( @"Sounds\Master.ogg" );
                    if( !File.Exists( strmas ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;
                
                case 6:
                    CDTXMania.Skin.soundBasic.t再生する();
                    string strbsc = CSkin.Path( @"Sounds\Basic.ogg" );
                    if( !File.Exists( strbsc ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                case 7:
                    CDTXMania.Skin.soundAdvanced.t再生する();
                    string stradv = CSkin.Path( @"Sounds\Advanced.ogg" );
                    if( !File.Exists( stradv ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                case 8:
                    CDTXMania.Skin.soundExtreme.t再生する();
                    string strext = CSkin.Path( @"Sounds\Extreme.ogg" );
                    if( !File.Exists( strext ) )
                        CDTXMania.Skin.sound変更音.t再生する();
                    break;

                default:
                    CDTXMania.Skin.sound変更音.t再生する();
                    break;
            }

			// 選曲ステージに変更通知を発出し、関係Activityの対応を行ってもらう。

			CDTXMania.stage選曲.t選択曲変更通知();
		}

        
        public void tラベル名からステータスパネルを決定する(string strラベル名)
        {
            if (string.IsNullOrEmpty(strラベル名))
            {
                this.nIndex = 0;
            }
            else
            {
                STATUSPANEL[] array = this.stパネルマップ;
                for (int i = 0; i < array.Length; i++)
                {
                    STATUSPANEL sTATUSPANEL = array[i];
                    if (strラベル名.Equals(sTATUSPANEL.label, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.nIndex = sTATUSPANEL.status;
                        return;
                    }
                    this.nIndex++;
                }
            }
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

            if( this.tx選択されている曲の曲名 != null )
            {
                this.tx選択されている曲の曲名.Dispose();
                this.tx選択されている曲の曲名 = null;
            }
            if( this.tx選択されている曲のアーティスト名 != null )
            {
                this.tx選択されている曲のアーティスト名.Dispose();
                this.tx選択されている曲のアーティスト名 = null;
            }
		}

		// CActivity 実装

		public override void On活性化()
		{
			if( this.b活性化してる )
				return;

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
            //this.prvFont = new CPrivateFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 28, FontStyle.Regular );

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
            if( this.prvFont != null )
                this.prvFont.Dispose();

			for( int i = 0; i < 13; i++ )
				this.ct登場アニメ用[ i ] = null;

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;

            this.tx選曲パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_image_panel_guitar.png" ) );

            if ( this.tx選曲パネル == null || CDTXMania.ConfigIni.bDrums有効 )
                this.tx選曲パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_image_panel.png"));

            this.txパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_music panel.png"));
            //this.tx帯 = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_backpanel.png" ) );
            this.txパネル帯 = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_Backbar.png" ) );
            //this.tx色帯 = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_ColorBar.png" ) );
            //this.txランプ用帯 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_lamppanel.png" ));
            
            this.txクリアランプ = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_Clearlamp.png") );
            #region[ テクスチャの復元 ]
            int nKeys = this.dicThumbnail.Count;
            string[] keys = new string[ nKeys ];
            this.dicThumbnail.Keys.CopyTo( keys, 0 );
            foreach (var key in keys)
                this.dicThumbnail[ key ] = this.tパスを指定してサムネイル画像を生成して返す( 0, key, this.stバー情報[ 0 ].eバー種別  );;

            //ここは最初に表示される画像の復元に必要。
            for (int i = 0; i < 13; i++)
            {
                this.t曲名バーの生成(i, this.stバー情報[i].strタイトル文字列, this.stバー情報[i].col文字色);
                this.tアーティスト名テクスチャの生成( i, this.stバー情報[ i ].strアーティスト名 );
                this.tパネルの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].strアーティスト名, this.stバー情報[ i ].col文字色 );
                //this.tパスを指定してサムネイル画像を生成する(i, this.stバー情報[i].strDTXフォルダのパス, this.stバー情報[i].eバー種別);
                if( this.stバー情報[ i ].strPreimageのパス != null )
                {
                    if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
			        {
    				    //txTumbnail = this.tサムネイルテクスチャを作成する( Path.GetDirectoryName( song.ScoreFile ) );
                        this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
				        this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
				    }
                    txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                }
            }
            #endregion


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
				using ( Bitmap image = new Bitmap( 640, 96 ) )
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

            for ( int i = 0; i < 13; i++ )
            {
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txタイトル名 );
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txアーティスト名 );
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txカスタム曲名テクスチャ );
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txカスタムアーティスト名テクスチャ );
            }

            if( this.tx選択されている曲の曲名 != null )
            {
                this.tx選択されている曲の曲名.Dispose();
                this.tx選択されている曲の曲名 = null;
            }
            if( this.tx選択されている曲のアーティスト名 != null )
            {
                this.tx選択されている曲のアーティスト名.Dispose();
                this.tx選択されている曲のアーティスト名 = null;
            }
            #region[ ジャケット画像の解放 ]
            int nKeys = this.dicThumbnail.Count;
			string[] keys = new string[ nKeys ];
			this.dicThumbnail.Keys.CopyTo( keys, 0 );
			foreach( var key in keys )
			{
				C共通.tDisposeする( this.dicThumbnail[ key ] );
				this.dicThumbnail[ key ] = null;
            }
            #endregion

			CDTXMania.t安全にDisposeする( ref this.txEnumeratingSongs );
			CDTXMania.t安全にDisposeする( ref this.txSongNotFound );
            CDTXMania.t安全にDisposeする( ref this.tx選曲パネル );
            CDTXMania.t安全にDisposeする( ref this.txパネル );
            CDTXMania.t安全にDisposeする( ref this.txクリアランプ );
            //CDTXMania.t安全にDisposeする( ref this.tx帯 );
            CDTXMania.t安全にDisposeする( ref this.txパネル帯 );
            //CDTXMania.t安全にDisposeする( ref this.tx色帯 );
            //CDTXMania.t安全にDisposeする( ref this.txランプ用帯 );

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
				long n現在時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
				
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
                        this.stバー情報[ index ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.アーティスト名;
						this.stバー情報[ index ].col文字色 = song.col文字色;
                        this.stバー情報[ index ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                        this.stバー情報[ index ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );
                        this.tアーティスト名テクスチャの生成( index, this.stバー情報[ index ].strアーティスト名 );
                        this.tパネルの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].strアーティスト名, this.stバー情報[ index ].col文字色 );

                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
				        {
					        //txTumbnail = this.tサムネイルテクスチャを作成する( Path.GetDirectoryName( song.ScoreFile ) );
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
					        this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
				        }
                        txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];


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

						this.t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        if( this.tx選択されている曲の曲名 != null )
                        {
                            this.tx選択されている曲の曲名.Dispose();
                            this.tx選択されている曲の曲名 = null;
                        }
                        if( this.tx選択されている曲のアーティスト名 != null )
                        {
                            this.tx選択されている曲のアーティスト名.Dispose();
                            this.tx選択されている曲のアーティスト名 = null;
                        }

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
                        this.stバー情報[ index ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す(song) ].譜面情報.アーティスト名;
						this.stバー情報[ index ].col文字色 = song.col文字色;
                        this.stバー情報[ index ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                        this.stバー情報[ index ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );
                        this.tアーティスト名テクスチャの生成( index, this.stバー情報[ index ].strアーティスト名 );
                        this.tパネルの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].strアーティスト名, this.stバー情報[ index ].col文字色 );

                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
				        {
					        //txTumbnail = this.tサムネイルテクスチャを作成する( Path.GetDirectoryName( song.ScoreFile ) );
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
					        this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
				        }
                        txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];


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


						this.t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        if( this.tx選択されている曲の曲名 != null )
                        {
                            this.tx選択されている曲の曲名.Dispose();
                            this.tx選択されている曲の曲名 = null;
                        }
                        if( this.tx選択されている曲のアーティスト名 != null )
                        {
                            this.tx選択されている曲のアーティスト名.Dispose();
                            this.tx選択されている曲のアーティスト名 = null;
                        }
						
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
						this.txEnumeratingSongs.t2D描画( CDTXMania.app.Device, 530, 240 );
					}
				}
				else
				{
					if ( this.txSongNotFound != null )
						this.txSongNotFound.t2D描画( CDTXMania.app.Device, 500, 190 );
				}
				//-----------------
				#endregion

				return 0;
			}
            var bar = SlimDX.Matrix.Identity;

            var barL = SlimDX.Matrix.Identity;

            Matrix[] barC = new Matrix[2];
            Matrix[] barLa = new Matrix[2];
            Matrix[] barU = new Matrix[2];

            #region[ビルボードの実験コード。「ここでするなよ...」とか言わないであげて...]
            //Z座標はプラス方向にすると動かなくなるので注意。
            
            //共通
            Vector3 VecCam = new Vector3( 0.0f, 0.0f, -1.0f ); //Zをマイナス方向にしないと描画されない(?)
            Vector3 VecTarg = new Vector3( 0.0f, 0.0f, 0.0f );
            Vector3 VecUp = new Vector3( 0.0f, 1.0f, 0.0f );

            bar *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
            barL *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );

            bar *= SlimDX.Matrix.RotationYawPitchRoll( -0.40f, 0.0f, 0.0f ); ////通常のRotationと何が違うのかわからない(Axisから倍率を抜いただけ?)
            barL *= SlimDX.Matrix.RotationYawPitchRoll( 0.40f, 0.0f, 0.0f );


            //bar *= SlimDX.Matrix.PerspectiveFovLH( C変換.DegreeToRadian(45), 1280.0f/720.0f, 1.0f, 500000.0f ); //カメラの視野角など調整
            
            //ワールド変換とか意味ワカンネ
            #endregion

            //bar *= SlimDX.Matrix.RotationY(-0.415f);
            //barL *= SlimDX.Matrix.RotationY(0.415f);
            //bar *= SlimDX.Matrix.Translation(540f, 20f, -24f);
            bar *= SlimDX.Matrix.Translation(600f, 121f, 190f);
            barL *= SlimDX.Matrix.Translation(-600f, 121f, 190f);
            //barL *= SlimDX.Matrix.Translation(-540f, 20f, -24f);
            //bar *= SlimDX.Matrix.Scaling(1.0f, 0.65f, 1.0f);
            //bar *= SlimDX.Matrix.Scaling( 1.0f, 1.0f, 1.0f );
            //barL *= SlimDX.Matrix.Scaling(1.0f, 0.65f, 1.0f);

            //bar *= SlimDX.Matrix.RotationAxis( new Vector3( 0f, -0.415f, 0f ), 0.5f );

            for( int i = 0; i < 2; i++ )
            {
                float fRate = 1.0f;
                if( i == 0 ) fRate = -1.0f;

                barC[ i ] = Matrix.Identity;
                barC[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barC[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barC[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, 148f, 186f);

                barLa[ i ] = Matrix.Identity;
                barLa[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barLa[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barLa[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, 100f, 186f);

                barU[ i ] = Matrix.Identity;
                barU[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barU[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barU[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, -106f, 186f);
            }


            this.txパネル帯.t3D描画( CDTXMania.app.Device, barU[ 0 ], new Rectangle( 2, 74, 1000, 30 ) );
            this.txパネル帯.t3D描画( CDTXMania.app.Device, barU[ 1 ], new Rectangle( 2, 74, 1000, 30 ) );
            this.txパネル帯.t3D描画( CDTXMania.app.Device, bar, new Rectangle( 2, 38, 1000, 30 ) );   //右の帯。
            this.txパネル帯.t3D描画( CDTXMania.app.Device, barL, new Rectangle( 2, 38, 1000, 30 ) );   //左の帯。
            //this.tx帯.t3D描画(CDTXMania.app.Device, barL);   //左の帯。
            //this.txパネル帯.n透明度 = 155 + this.ct登場アニメ用[10].n現在の値;
            this.txパネル帯.t3D描画( CDTXMania.app.Device, barC[ 0 ], new Rectangle( 2, 2, 1000, 30 ) );
            this.txパネル帯.t3D描画( CDTXMania.app.Device, barC[ 1 ], new Rectangle( 2, 2, 1000, 30 ) );

			#region [ デバッグ補助 ]
			//-----------------
            /*
            stマトリックス座標 = new ST中心点[] {            
            new ST中心点() { x = -980.0000f, y = 4f, z = 350f, rotY = 0.4000f },
			new ST中心点() { x = -780.0000f, y = 4f, z = 264f, rotY = 0.4000f },
            new ST中心点() { x = -590.0000f, y = 4f, z = 174f, rotY = 0.4000f },
			new ST中心点() { x = -400.0000f, y = 4f, z = 92f, rotY = 0.4000f },
			new ST中心点() { x = -210.0000f, y = 4f, z = 10f, rotY = 0.4000f },
			new ST中心点() { x = 6.00002622683f, y = 2f, z = 0f, rotY = 0f }, 
			new ST中心点() { x = 210.0000f, y = 2f, z = 10f, rotY = -0.4000f },
            new ST中心点() { x = 400.0000f, y = 2f, z = 92f, rotY = -0.4f },
            new ST中心点() { x = 590.0000f, y = 2f, z = 174f, rotY = -0.4f },
            new ST中心点() { x = 780.0000f, y = 2f, z = 264f, rotY = -0.4f },
            new ST中心点() { x = 980.0000f, y = 2f, z = 350f, rotY = -0.4f },
            new ST中心点() { x = 1200.0000f, y = 2f, z = 450f, rotY = -0.4f },
            new ST中心点() { x = 1500.0000f, y = 2f, z = -289.5575f, rotY = -0.9279888f },
            new ST中心点() { x = 1500.0000f, y = 2f, z = -289.5575f, rotY = -0.9279888f },
		};
            */
            //-----------------
			#endregion

            #region[ ジャケット画像のMatrix配列 ]
            //パネル1枚1枚にMatrixを割り当ててみる。
            //とりあえず構造の最適化無しで地味に作ってみる。
            Matrix[] matSongPanel = new SlimDX.Matrix[ 13 ];
            Matrix[] matJacket = new SlimDX.Matrix[ 13 ];
            ST中心点[] st3D座標 = new ST中心点[] {
			#region [ 計算なんてしていない。 ]
		    	//-----------------        
                new ST中心点() { x = -820.0000f, y = 8f, z = 264f, rotY = 0.410f },
    			new ST中心点() { x = -666.0000f, y = 8f, z = 198f, rotY = 0.410f },
                new ST中心点() { x = -506.0000f, y = 8f, z = 127f, rotY = 0.410f },
		    	new ST中心点() { x = -350.0000f, y = 8f, z = 60f, rotY = 0.410f },
	    		new ST中心点() { x = -194.0000f, y = 8f, z = -6f, rotY = 0.410f },
    			new ST中心点() { x = 6.00002622683f, y = 8f, z = 0f, rotY = 0.0f }, 
			    new ST中心点() { x = 208.0000f, y = 8f, z = 0f, rotY = -0.410f },
                new ST中心点() { x = 362.0000f, y = 8f, z = 66f, rotY = -0.410f },
                new ST中心点() { x = 518.0000f, y = 8f, z = 132f, rotY = -0.410f },
                new ST中心点() { x = 676.0000f, y = 8f, z = 200.0f, rotY = -0.410f },
                new ST中心点() { x = 837.0000f, y = 8f, z = 270.0f, rotY = -0.410f },
                new ST中心点() { x = 1100.0000f, y = 8f, z = 340.0f, rotY = -0.410f },
                new ST中心点() { x = 1500.0000f, y = 8f, z = 450.0f, rotY = -0.410f },
                new ST中心点() { x = 1500.0000f, y = 8f, z = 340.0f, rotY = -0.410f },
			    //-----------------
			#endregion
		    };

            for( int i = 0; i < 13; i++ )
            {
                matSongPanel[ i ] = Matrix.Identity;
                matSongPanel[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                matSongPanel[ i ] *= SlimDX.Matrix.Scaling(0.62f, 0.88f, 1.0f);
                //matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                //matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );


                matJacket[ i ] = Matrix.Identity;
                matJacket[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                //matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f ); //ジャケット画像は計算順序を変える。

                //matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, 0f, st3D座標[ i ].z );
            }
            #endregion



			if( !this.b登場アニメ全部完了 )
			{
				#region [ (1) 登場アニメフェーズの描画。]
				//-----------------
				for( int i = 0; i < 13; i++ )	// パネルは全13枚。
				{
					if( this.ct登場アニメ用[ i ].n現在の値 >= 0 )
					{
						int nパネル番号 = ( ( ( this.n現在の選択行 - 5 ) + i ) + 13 ) % 13;

                        if( i == 6 )
                        {
                            #region [ ジャケット画像の描画 ]
                            //-----------------
                            if( this.stバー情報[ nパネル番号 ].txパネル != null )
                            {
                                matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                                this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                            }
                            if( this.txTumbnail[nパネル番号] != null )
                            {
                                float f拡大率 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width;
                                float f拡大率2 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;
                                //var mat = SlimDX.Matrix.Identity;
                                //mat *= SlimDX.Matrix.Scaling(f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f);
                                //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                //mat *= SlimDX.Matrix.Translation(
                                //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].y + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率 - 1f,
                                //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.txTumbnail[nパネル番号].t3D描画(CDTXMania.app.Device, mat);
                                
                                matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                                matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y - 1.5f, st3D座標[ i ].z );
 
                                this.txTumbnail[ nパネル番号 ].t3D描画(CDTXMania.app.Device, matJacket[ i ] );
                            }
                            //-----------------
                            #endregion
                            #region [ タイトル名テクスチャを描画。]
                            //-----------------
                            if( this.stバー情報[nパネル番号].txタイトル名 != null )
                            {
                                //this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, x + 0x58, y + 8 );
                                var mat = SlimDX.Matrix.Identity;
                                mat *= SlimDX.Matrix.Scaling( 0.35f, 0.45f, 1.0f );
                                mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[ i ].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                mat *= SlimDX.Matrix.Translation(
                                    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                    (this.stマトリックス座標[i].y + 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.stバー情報[nパネル番号].txタイトル名.t3D描画(CDTXMania.app.Device, mat);
                            }
                            if (this.stバー情報[nパネル番号].txアーティスト名 != null)
                            {
                                var mat = SlimDX.Matrix.Identity;
                                mat *= SlimDX.Matrix.Scaling(0.35f, 0.45f, 1.0f);
                                mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                mat *= SlimDX.Matrix.Translation(
                                    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                    (this.stマトリックス座標[i].y - 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.stバー情報[nパネル番号].txアーティスト名.t3D描画(CDTXMania.app.Device, mat);
                            }
                            //-----------------
                            #endregion
                            if( this.tx選曲パネル != null )
                                this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 761, 233, new Rectangle( 304, 70, 59, 242 ) );
                        }
                        else if( i == 5 )
						{
							// (A) 選択曲パネルを描画。
                            if( this.tx選曲パネル != null )
                                this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 531, 243, new Rectangle( 74, 80, 230, 230 ) ); //真ん中の部分は別々に描画。
                            if( this.txパネル帯 != null )
                            {
                                this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 0 ], new Rectangle( 2, 110, 1000, 20 ) );
                            }
                            #region [ バーテクスチャを描画。]
							//-----------------
                            if( this.txパネル != null )
                            {
                                //var mat = SlimDX.Matrix.Identity;
                                //mat *= SlimDX.Matrix.Scaling(0.8f, 0.8f, 1.0f);
                                //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                //mat *= SlimDX.Matrix.Translation(
                                //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].y + 2 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.txパネル.t3D描画(CDTXMania.app.Device, mat);
                            }
                            if( this.tx選曲パネル != null )
                                this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 457, 163, new Rectangle( 0, 0, 363, 368 ) );
							//-----------------
							#endregion
                            #region [ ジャケット画像の描画 ]
                            //-----------------
                            for( int la = 0; la < 5 ; la++ )
                            {
                                if( this.txクリアランプ != null && CDTXMania.stage選曲.r現在選択中の曲.ar難易度ラベル[ la ] != null && CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ] != null )
                                    this.txクリアランプ.t2D描画(CDTXMania.app.Device, 506, 292 - la * 13, new Rectangle((CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), ( CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.フルコンボ.Drums ? 10 : 0), 11, 10));
                            }
                            if( this.txTumbnail[ nパネル番号 ] != null )
                            {
                                float f拡大率 = (float)218.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width;
                                float f拡大率2 = (float)218.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;
                                this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率2, 1.0f );
                                this.txTumbnail[ nパネル番号 ].t2D描画(CDTXMania.app.Device, 537, 249 );
                                this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1.0f );
                            }
                            //-----------------
                            #endregion
						    #region [ タイトル名テクスチャを描画。]
				            //-----------------
                            if( File.Exists( this.stバー情報[ nパネル番号 ].strDTXフォルダのパス + "TitleTexture.png" ) && this.tx選択されている曲の曲名 == null )
                            {
                                this.tx選択されている曲の曲名 = this.tカスタム曲名の生成( nパネル番号 );
                            }
                            else
                            {
                                if( this.stバー情報[ nパネル番号 ].strタイトル文字列 != "" && this.stバー情報[ nパネル番号 ].strタイトル文字列 != null && this.tx選択されている曲の曲名 == null )
                                    this.tx選択されている曲の曲名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ nパネル番号 ].strタイトル文字列 );
                            }
                            if( File.Exists( this.stバー情報[ nパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) && this.tx選択されている曲のアーティスト名 == null )
                            {
                                this.tx選択されている曲のアーティスト名 = this.tカスタムアーティスト名テクスチャの生成( nパネル番号 );

                                this.tx選択されている曲のアーティスト名.t2D描画(CDTXMania.app.Device, 552, 470);
                            }
                            else
                            {
                                if( this.stバー情報[ nパネル番号 ].strアーティスト名 != "" && this.stバー情報[ nパネル番号 ].strアーティスト名 != null && this.tx選択されている曲のアーティスト名 == null )
                                    this.tx選択されている曲のアーティスト名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ nパネル番号 ].strアーティスト名 );

                                if( this.tx選択されている曲のアーティスト名 != null )
                                    this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, 770 - this.stバー情報[ nパネル番号 ].nアーティスト名テクスチャの長さdot, 470);
                            }

    						if( this.tx選択されている曲の曲名 != null )
	    						this.tx選択されている曲の曲名.t2D描画( CDTXMania.app.Device, 552, 210 );

                            //if( this.stバー情報[ nパネル番号 ].txタイトル名 != null )
							//	this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, 556, 210 );
                            //if (this.stバー情報[ nパネル番号 ].txアーティスト名 != null)
                            //    this.stバー情報[ nパネル番号 ].txアーティスト名.t2D描画(CDTXMania.app.Device, 560 - 770 - this.stバー情報[ nパネル番号 ].nアーティスト名テクスチャの長さdot, 402);
							//-----------------
							#endregion
						}
						else
						{
							// (B) その他のパネルの描画。
                            if( this.txパネル帯 != null )
                                this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 1 ], new Rectangle( 2, 110, 1000, 20 ) );

                            #region [ ジャケット画像の描画 ]
                            //-----------------
                            if( this.stバー情報[ nパネル番号 ].txパネル != null )
                            {
                                matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                                this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                            }
                            if( this.txTumbnail[nパネル番号] != null )
                            {
                                float f拡大率 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width ;
                                float f拡大率2 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;

                                matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                                matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y - 1.5f, st3D座標[ i ].z );
                                //matJacket[ i ] *= SlimDX.Matrix.Translation( fX, fY - 1.5f, fZ );

                                this.txTumbnail[nパネル番号].t3D描画( CDTXMania.app.Device, matJacket[ i ] );
                            }
                            //-----------------
                            #endregion
							#region [ タイトル名テクスチャを描画。]
							//-----------------
                            if( this.tx選曲パネル != null )
                                this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 761, 233, new Rectangle( 304, 70, 59, 242 ) );
                            if (this.stバー情報[ nパネル番号 ].txタイトル名 != null)
                            {
                                //var mat = SlimDX.Matrix.Identity;
                                //mat *= SlimDX.Matrix.Scaling(0.35f, 0.45f, 1.0f);
                                //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                //mat *= SlimDX.Matrix.Translation(
                                //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].y + 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.stバー情報[nパネル番号].txタイトル名.t3D描画(CDTXMania.app.Device, mat);
                            }
                            if (this.stバー情報[nパネル番号].txアーティスト名 != null)
                            {
                                //var mat = SlimDX.Matrix.Identity;
                                //mat *= SlimDX.Matrix.Scaling(0.35f, 0.45f, 1.0f);
                                //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                                //mat *= SlimDX.Matrix.Translation(
                                //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].y - 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                                //this.stバー情報[nパネル番号].txアーティスト名.t3D描画(CDTXMania.app.Device, mat);
                            }
							//-----------------
							#endregion
						}
					}
				}
				//-----------------
				#endregion

			}
			else
			{
                //CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, this.n現在のスクロールカウンタ.ToString() );
				#region [ (2) 通常フェーズの描画。]
				//-----------------
				for( int i = 0; i < 13; i++ )	// パネルは全13枚。
				{
					int nパネル番号 = ( ( ( this.n現在の選択行 - 5 ) + i ) + 13 ) % 13;
					int n見た目の行番号 = i;
					int n次のパネル番号 = ( this.n現在のスクロールカウンタ <= 0 ) ? ( ( i + 1 ) % 13 ) : ( ( ( i - 1 ) + 13 ) % 13 );
					int x = this.ptバーの基本座標[ n見た目の行番号 ].X + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].X - this.ptバーの基本座標[ n見た目の行番号 ].X ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
					int y = this.ptバーの基本座標[ n見た目の行番号 ].Y + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].Y - this.ptバーの基本座標[ n見た目の行番号 ].Y ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
                    //float fX = this.n現在のスクロールカウンタ <= 0 ? this.stマトリックス座標[ n見た目の行番号 ].x + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].x - this.stマトリックス座標[ n見た目の行番号 ].x ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) ) : 
                    //                                                 this.stマトリックス座標[ n見た目の行番号 ].x + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].x + this.stマトリックス座標[ n見た目の行番号 ].x ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) );

                    //float fY = this.n現在のスクロールカウンタ <= 0 ? this.stマトリックス座標[ n見た目の行番号 ].y + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].y - this.stマトリックス座標[ n見た目の行番号 ].y ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) ) :
                    //                                                 this.stマトリックス座標[ n見た目の行番号 ].y + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].y - this.stマトリックス座標[ n見た目の行番号 ].y ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) );

                    //float fZ = this.n現在のスクロールカウンタ <= 0 ? this.stマトリックス座標[ n見た目の行番号 ].z + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].z - this.stマトリックス座標[ n見た目の行番号 ].z ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) ) :
                    //                                                 this.stマトリックス座標[ n見た目の行番号 ].z + ( ( ( this.stマトリックス座標[ n次のパネル番号 ].z + this.stマトリックス座標[ n見た目の行番号 ].z ) * ( (  this.n現在のスクロールカウンタ ) ) / 100.0f  ) );
                    
                    float fX = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].x + ( ( ( st3D座標[ n次のパネル番号 ].x - st3D座標[ n見た目の行番号 ].x ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) : 
                                                                     st3D座標[ n見た目の行番号 ].x + ( ( ( st3D座標[ n次のパネル番号 ].x - st3D座標[ n見た目の行番号 ].x ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fY = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].y + ( ( ( st3D座標[ n次のパネル番号 ].y + st3D座標[ n見た目の行番号 ].y ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].y + ( ( ( st3D座標[ n次のパネル番号 ].y + st3D座標[ n見た目の行番号 ].y ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fZ = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].z + ( ( ( st3D座標[ n次のパネル番号 ].z - st3D座標[ n見た目の行番号 ].z ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].z + ( ( ( st3D座標[ n次のパネル番号 ].z - st3D座標[ n見た目の行番号 ].z ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fR = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].rotY + ( ( ( st3D座標[ n次のパネル番号 ].rotY - st3D座標[ n見た目の行番号 ].rotY ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].rotY + ( ( ( st3D座標[ n次のパネル番号 ].rotY - st3D座標[ n見た目の行番号 ].rotY ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );


                    if (i == 6)
                    {
                        #region [ ジャケット画像の描画 ]
                        //-----------------
                        if( this.stバー情報[ nパネル番号 ].txパネル != null )
                        {
                            //matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( fR, 0.0f, 0.0f );
                            matSongPanel[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y, fZ );
                            this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                        }
                        if (this.txTumbnail[ nパネル番号 ] != null)
                        {
                            float f拡大率 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width;
                            float f拡大率2 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;
                            //var mat = SlimDX.Matrix.Identity;
                            //mat *= SlimDX.Matrix.Scaling(f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f);

                            matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                            //matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( fR, 0.0f, 0.0f );
                            //matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                            matJacket[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y - 1.5f, fZ );

                            this.txTumbnail[ nパネル番号 ].t3D描画(CDTXMania.app.Device, matJacket[ i ] );
                        }
                        //-----------------
                        #endregion
                        #region [ タイトル名テクスチャを描画。]
                        //-----------------
                        if (this.stバー情報[nパネル番号].txタイトル名 != null)
                        {
                            //this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, x + 0x58, y + 8 );
                            var mat = SlimDX.Matrix.Identity;
                            mat *= SlimDX.Matrix.Scaling(0.35f, 0.45f, 1.0f);
                            mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                            mat *= SlimDX.Matrix.Translation(
                                (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                (this.stマトリックス座標[i].y + 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                            //this.stバー情報[nパネル番号].txタイトル名.t3D描画(CDTXMania.app.Device, mat);
                        }
                        if (this.stバー情報[nパネル番号].txアーティスト名 != null)
                        {
                            var mat = SlimDX.Matrix.Identity;
                            mat *= SlimDX.Matrix.Scaling(0.35f, 0.45f, 1.0f);
                            mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                            mat *= SlimDX.Matrix.Translation(
                                (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                                (this.stマトリックス座標[i].y - 110 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                                (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                            //this.stバー情報[nパネル番号].txアーティスト名.t3D描画(CDTXMania.app.Device, mat);
                        }
                        //-----------------
                        #endregion
                    }
                    else if( ( i == 5 ) )
                    {

                        if( this.txパネル帯 != null )
                        {
                            this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 0 ], new Rectangle( 2, 110, 1000, 20 ) );
                        }
                        //for (int la = 0; la < 5; la++)
                        {
                            //if( this.stバー情報[ 6 ].ar難易度ラベル[ la ] != null )
                            {
                                //var lamp = SlimDX.Matrix.Identity;
                                //lamp *= SlimDX.Matrix.Translation(
                                //( this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x ) )) * CTexture.f画面比率,
                                //( this.stマトリックス座標[i].y + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y ) ) ) * CTexture.f画面比率 - 1f - la * 13f,
                                //( this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z ) ) ) * CTexture.f画面比率);

                                //this.txクリアランプ.t3D描画( CDTXMania.app.Device, lamp, new Rectangle((CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), (CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.フルコンボ.Drums ? 10 : 0), 11, 10));
                            }
                        }
                        //if( this.tx選曲パネル != null )
                        //    this.tx選曲パネル.t2D描画(CDTXMania.app.Device, 531, 243, new Rectangle(74, 80, 230, 230)); //真ん中の部分は別々に描画。
						// (A) スクロールが停止しているときの選択曲バーの描画。
                        #region [ ジャケット画像の描画 ]
                        //-----------------
                        //if( this.tx選曲パネル != null )
                        //    this.tx選曲パネル.t2D描画(CDTXMania.app.Device, 457, 163, new Rectangle(0, 0, 363, 368));

                        //for( int la = 0; la < 5 ; la++ )
                        //{
                        //    if( this.txクリアランプ != null && CDTXMania.stage選曲.r現在選択中の曲.ar難易度ラベル[ la ] != null && CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ] != null )
                        //        this.txクリアランプ.t2D描画(CDTXMania.app.Device, 506, 292 - la * 13, new Rectangle((CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), ( CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.フルコンボ.Drums ? 10 : 0), 11, 10));
                        //}
                        //if( this.txTumbnail[ nパネル番号 ] != null )
                        //{
                        //    float f拡大率 = (float)218.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width;
                        //    float f拡大率2 = (float)218.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;
                        //    this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率2, 1.0f );
                        //    this.txTumbnail[ nパネル番号 ].t2D描画(CDTXMania.app.Device, 537, 249 );
                        //    this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1.0f );
                        //}
                        //-----------------
                        #endregion
						#region [ タイトル名テクスチャを描画。]
						//-----------------

                        //if( File.Exists( this.stバー情報[ nパネル番号 ].strDTXフォルダのパス + "TitleTexture.png" ) && this.tx選択されている曲の曲名 == null )
                        //{
                        //    this.tx選択されている曲の曲名 = this.tカスタム曲名の生成( nパネル番号 );
                        //}
                        //else
                        //{
                        //    if( this.stバー情報[ nパネル番号 ].strタイトル文字列 != "" && this.stバー情報[ nパネル番号 ].strタイトル文字列 != null && this.tx選択されている曲の曲名 == null )
                        //        this.tx選択されている曲の曲名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ nパネル番号 ].strタイトル文字列 );
                        //}
                        //if( File.Exists( this.stバー情報[ nパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) && this.tx選択されている曲のアーティスト名 == null )
                        //{
                        //    this.tx選択されている曲のアーティスト名 = this.tカスタムアーティスト名テクスチャの生成( nパネル番号 );

                        //    if (this.tx選択されている曲のアーティスト名 != null)
                        //        this.tx選択されている曲のアーティスト名.t2D描画(CDTXMania.app.Device, 552, 470);
                        //}
                        //else
                        //{
                        //    if( this.stバー情報[ nパネル番号 ].strアーティスト名 != "" && this.stバー情報[ nパネル番号 ].strアーティスト名 != null && this.tx選択されている曲のアーティスト名 == null )
                        //        this.tx選択されている曲のアーティスト名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ nパネル番号 ].strアーティスト名 );


                        //}

                        //if( this.tx選択されている曲の曲名 != null )
                        //    this.tx選択されている曲の曲名.t2D描画( CDTXMania.app.Device, 552, 210 );
                        //if( this.tx選択されている曲のアーティスト名 != null )
                        //    this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, 770 - this.stバー情報[ nパネル番号 ].nアーティスト名テクスチャの長さdot, 470 );

                        //if( this.stバー情報[ nパネル番号 ].txタイトル名 != null )
						//	  this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, 556, 210 );
                        //if (this.stバー情報[ nパネル番号 ].txアーティスト名 != null)
                        //    this.stバー情報[ nパネル番号 ].txアーティスト名.t2D描画(CDTXMania.app.Device, 560 - 770 - this.stバー情報[ nパネル番号 ].nアーティスト名テクスチャの長さdot, 402);
						//-----------------
						#endregion
					}
                    else if (i > 10)
                    {
                    }
                    else
                    {
                        // (B) スクロール中の選択曲バー、またはその他のバーの描画。
                        if( this.txパネル帯 != null )
                            this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 1 ], new Rectangle( 2, 110, 1000, 20 ) );
                            //this.txランプ用帯.t3D描画( CDTXMania.app.Device, bar );   //右の帯。
                        if( this.tx選曲パネル != null )
                            this.tx選曲パネル.t2D描画(CDTXMania.app.Device, 761, 233, new Rectangle(304, 70, 59, 242));

                        #region [ ジャケット画像の描画 ]
                        //-----------------
                        if( this.stバー情報[ nパネル番号 ].txパネル != null )
                        {
                            //var mat = SlimDX.Matrix.Identity;
                            //mat *= SlimDX.Matrix.Scaling(0.62f, 0.88f, 1.0f);
                            //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                            //mat *= SlimDX.Matrix.Translation(
                            //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                            //    (this.stマトリックス座標[i].y + 2 + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                            //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                            //this.stバー情報[nパネル番号].txパネル.t3D描画(CDTXMania.app.Device, mat);
                            matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            matSongPanel[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y, fZ );
                            this.stバー情報[nパネル番号].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                        }
                        if( this.txTumbnail[nパネル番号] != null )
                        {
                            float f拡大率 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width ;
                            float f拡大率2 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;
                            //var mat = SlimDX.Matrix.Identity;
                            //mat *= SlimDX.Matrix.Scaling(f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f);
                            //mat *= SlimDX.Matrix.RotationY(this.stマトリックス座標[i].rotY + (this.stマトリックス座標[i].rotY - this.stマトリックス座標[i].rotY));
                            //mat *= SlimDX.Matrix.Translation(
                            //    (this.stマトリックス座標[i].x + (int)((this.stマトリックス座標[i].x - this.stマトリックス座標[i].x))) * CTexture.f画面比率,
                            //    (this.stマトリックス座標[i].y + (int)((this.stマトリックス座標[i].y - this.stマトリックス座標[i].y))) * CTexture.f画面比率,
                            //    (this.stマトリックス座標[i].z + (int)((this.stマトリックス座標[i].z - this.stマトリックス座標[i].z))) * CTexture.f画面比率);
                            //this.txTumbnail[nパネル番号].t3D描画(CDTXMania.app.Device, mat);

                            matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                            matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            //matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                            matJacket[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y - 1.5f, fZ );

                            this.txTumbnail[nパネル番号].t3D描画( CDTXMania.app.Device, matJacket[ i ] );
                        }
                        //-----------------
                        #endregion
                    }
				}

                //選択中の曲
                int n選択曲のパネル番号 = ( ( ( this.n現在の選択行 - 5 ) + 5 ) + 13 ) % 13;

                #region[ 中央パネル ]
                if( this.tx選曲パネル != null )
                    this.tx選曲パネル.t2D描画(CDTXMania.app.Device, 457, 163, new Rectangle(0, 0, 363, 368));
                #endregion
                #region[ クリアランプ ]
                for( int la = 0; la < 5 ; la++ )
                {
                    if( this.txクリアランプ != null && CDTXMania.stage選曲.r現在選択中の曲.ar難易度ラベル[ la ] != null && CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ] != null )
                        this.txクリアランプ.t2D描画(CDTXMania.app.Device, 506, 292 - la * 13, new Rectangle((CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), ( CDTXMania.stage選曲.r現在選択中の曲.arスコア[la].譜面情報.フルコンボ.Drums ? 10 : 0), 11, 10));
                }
                #endregion
                #region[ ジャケット画像 ]
                if ( this.txTumbnail[ n選択曲のパネル番号 ] != null )
                {
                    float f拡大率 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Width;
                    float f拡大率2 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Height;
                    this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率2, 1.0f );
                    this.txTumbnail[ n選択曲のパネル番号 ].t2D描画(CDTXMania.app.Device, 537, 249 );
                    this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1.0f );
                }
                #endregion
                #region[ タイトル・アーティスト名 ]
                if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "TitleTexture.png" ) && this.tx選択されている曲の曲名 == null )
                {
                    this.tx選択されている曲の曲名 = this.tカスタム曲名の生成( n選択曲のパネル番号 );
                }
                else
                {
                    if( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != "" && this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != null && this.tx選択されている曲の曲名 == null )
                        this.tx選択されている曲の曲名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 );
                }
                if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) && this.tx選択されている曲のアーティスト名 == null )
                {
                    this.tx選択されている曲のアーティスト名 = this.tカスタムアーティスト名テクスチャの生成( n選択曲のパネル番号 );
                    if (this.tx選択されている曲のアーティスト名 != null)
                        this.tx選択されている曲のアーティスト名.t2D描画(CDTXMania.app.Device, 552, 470);
                }
                else
                {
                    if( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != "" && this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != null && this.tx選択されている曲のアーティスト名 == null )
                        this.tx選択されている曲のアーティスト名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 );
                }

		        if( this.tx選択されている曲の曲名 != null )
				    this.tx選択されている曲の曲名.t2D描画( CDTXMania.app.Device, 552, 210 );
                if( this.tx選択されている曲のアーティスト名 != null )
                    this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, 770 - this.stバー情報[ n選択曲のパネル番号 ].nアーティスト名テクスチャの長さdot, 470 );
                #endregion

                //-----------------
				#endregion

			}
			#region [ スクロール地点の計算(描画はCActSelectShowCurrentPositionにて行う) #27648 ]
			int py;
			double d = 0;
			if ( nNumOfItems > 1 )
			{
				d = ( 336 - 8 ) / (double) ( nNumOfItems - 1 );
				py = (int) ( d * ( nCurrentPosition - 1 ) );
			}
			else
			{
				d = 0;
				py = 0;
			}
			int delta = (int) ( d * this.n現在のスクロールカウンタ / 100 );
			if ( py + delta <= 336 - 8 )
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
		private enum Eバー種別 { Score, Box, Other, Random, BackBox }

		private struct STバー
		{
			public CTexture Score;
			public CTexture Box;
			public CTexture Other;
            public CTexture Random;
            public CTexture BackBox;
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

                        case 3:
                            return this.Random;

                        case 4:
                            return this.BackBox;
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

                        case 3:
                            this.Random = value;
                            return;

                        case 4:
                            this.BackBox = value;
                            return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		private struct STバー情報
		{
			public CActSelect曲リスト.Eバー種別 eバー種別;
			public string strタイトル文字列;
            public string strアーティスト名;
			public CTexture txタイトル名;
            public CTexture txアーティスト名;
            public CTexture txパネル;
            public CTexture txカスタム曲名テクスチャ;
            public CTexture txカスタムアーティスト名テクスチャ;
            public int nアーティスト名テクスチャの長さdot;
            public int nタイトル名テクスチャの長さdot;
			public STDGBVALUE<int> nスキル値;
			public Color col文字色;
            public string strDTXフォルダのパス;
            public string strPreimageのパス;
            public Cスコア.ST譜面情報 ar譜面情報;
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

        protected struct ST中心点
        {
            public float x;
            public float y;
            public float z;
            public float rotY;
        }
		/// <summary>
		/// <para>SSTFファイル絶対パス(key)とサムネイル画像(value)との辞書。</para>
		/// <para>アプリの起動から終了まで単純に増加を続け、要素が減ることはない。</para>
		/// </summary>
		protected Dictionary<string, CTexture> dicThumbnail = new Dictionary<string, CTexture>();
		
		/// <summary>
		/// <para>SSTFファイル絶対パス(key)とプロパティ画像(value)との辞書。</para>
		/// <para>アプリの起動から終了まで単純に増加を続け、要素が減ることはない。</para>
		/// </summary>
		protected Dictionary<string, CTexture> dicProperty = new Dictionary<string, CTexture>();
        
        protected ST中心点[] stマトリックス座標 = new ST中心点[] {
			#region [ 実は円弧配置になってない。射影行列間違ってるよスターレインボウ見せる気かよ… ]
			//-----------------        
            new ST中心点() { x = -940.0000f, y = 4f, z = 320f, rotY = 0.4150f },
			new ST中心点() { x = -740.0000f, y = 4f, z = 230f, rotY = 0.4150f },
            new ST中心点() { x = -550.0000f, y = 4f, z = 150f, rotY = 0.4150f },
			new ST中心点() { x = -370.0000f, y = 4f, z = 70f, rotY = 0.4150f },
			new ST中心点() { x = -194.0000f,     y = 4f, z = -6f, rotY = 0.4150f },
			new ST中心点() { x = 6.00002622683f, y = 2f, z = 0f, rotY = 0f }, 
			new ST中心点() { x = 204.0000f, y = 4f, z = 0f, rotY = -0.4150f },
            new ST中心点() { x = 362.0000f, y = 4f, z = 70f, rotY = -0.4150f },
            new ST中心点() { x = 528.0000f, y = 4f, z = 146f, rotY = -0.4150f },
            new ST中心点() { x = 686.0000f, y = 4f, z = 212f, rotY = -0.4150f },
            new ST中心点() { x = 848.0000f, y = 4f, z = 282f, rotY = -0.4150f },
            new ST中心点() { x = 1200.0000f, y = 4f, z = 450f, rotY = -0.4150f },
            new ST中心点() { x = 1500.0000f, y = 4f, z = -289.5575f, rotY = -0.9279888f },
            new ST中心点() { x = 1500.0000f, y = 4f, z = -289.5575f, rotY = -0.9279888f },
			//-----------------
			#endregion
		};

        protected readonly ST中心点[] stマトリックス曲名座標 = new ST中心点[] {
			#region [ 実は円弧配置になってない。射影行列間違ってるよスターレインボウ見せる気かよ… ]
			//-----------------
            new ST中心点() { x = -980.0000f, y = 4f, z = 350f, rotY = 0.4000f },
			new ST中心点() { x = -780.0000f, y = 4f, z = 264f, rotY = 0.4000f },
            new ST中心点() { x = -590.0000f, y = 4f, z = 174f, rotY = 0.4000f },
			new ST中心点() { x = -400.0000f, y = 4f, z = 92f, rotY = 0.4000f },
			new ST中心点() { x = -210.0000f, y = 4f, z = 10f, rotY = 0.4000f },
			new ST中心点() { x = 6.00002622683f, y = 2f, z = 0f, rotY = 0f }, 
			new ST中心点() { x = 210.0000f, y = 2f, z = 10f, rotY = -0.4000f },
            new ST中心点() { x = 400.0000f, y = 2f, z = 92f, rotY = -0.4f },
            new ST中心点() { x = 590.0000f, y = 2f, z = 174f, rotY = -0.4f },
            new ST中心点() { x = 780.0000f, y = 2f, z = 264f, rotY = -0.4f },
            new ST中心点() { x = 980.0000f, y = 2f, z = 350f, rotY = -0.4f },
            new ST中心点() { x = 1200.0000f, y = 2f, z = 450f, rotY = -0.4f },
            new ST中心点() { x = 1500.0000f, y = 2f, z = -289.5575f, rotY = -0.9279888f },
            new ST中心点() { x = 1500.0000f, y = 2f, z = -289.5575f, rotY = -0.9279888f },
			//-----------------
			#endregion
		};
        [StructLayout(LayoutKind.Sequential)]
        public struct STATUSPANEL
        {
            public string label;
            public int status;
        }
        public int nIndex;
        public STATUSPANEL[] stパネルマップ;

		public bool b登場アニメ全部完了;
		private Color color文字影 = Color.FromArgb( 0x40, 10, 10, 10 );
		public CCounter[] ct登場アニメ用 = new CCounter[ 13 ];
		private Font ft曲リスト用フォント;
		private long nスクロールタイマ;
		private int n現在のスクロールカウンタ;
		private int n現在の選択行;
		private int n目標のスクロールカウンタ;
		private readonly Point[] ptバーの基本座標 = new Point[] { new Point(0x2c4, 30), new Point(0x272, 0x51), new Point(0x242, 0x84), new Point(0x222, 0xb7), new Point(0x210, 0xea), new Point(0x1d0, 0x127), new Point(0x224, 0x183), new Point(0x242, 0x1b6), new Point(0x270, 0x1e9), new Point(0x2ae, 540), new Point(0x314, 0x24f), new Point(0x3e4, 0x282), new Point(0x500, 0x2b5) };
		private STバー情報[] stバー情報 = new STバー情報[ 13 ];
		private CTexture txSongNotFound, txEnumeratingSongs;
		private CTexture txアイテム数数字;
        private CTexture[] txTumbnail = new CTexture[13];
        private CTexture tx選曲パネル;
        private CTexture txパネル;
        private CTexture txパネル帯;
        private CTexture tx選択されている曲の曲名;
        private CTexture tx選択されている曲のアーティスト名;
        private CTexture txクリアランプ;
        //CPrivateFont prvFont;
        private CPrivateFastFont prvFont;

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
						return Eバー種別.Box;
                    
                    case C曲リストノード.Eノード種別.BACKBOX:
                        return Eバー種別.BackBox;

                    case C曲リストノード.Eノード種別.RANDOM:
                        return Eバー種別.Random;
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
                this.stバー情報[ i ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.アーティスト名;
				this.stバー情報[ i ].col文字色 = song.col文字色;
				this.stバー情報[ i ].eバー種別 = this.e曲のバー種別を返す( song );
                this.stバー情報[ i ].ar譜面情報 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報;

                //for( int n = 0; n < 5; n++ )
                {
                    //this.stバー情報[ i ].ar難易度ラベル[ n ];

                    //if( this.stバー情報[ i ].ar難易度ラベル[ n ] != null )
                        //this.stバー情報[ i ].ar難易度ラベル[ n ] = song.ar難易度ラベル[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ];
                }
				
                this.stバー情報[ i ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                this.stバー情報[ i ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
                this.tパネルの生成(i, song.strタイトル, this.stバー情報[ i ].strアーティスト名, song.col文字色);
                if( this.stバー情報[ i ].strPreimageのパス != null )
                {
                    if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
				    {
			            //txTumbnail = this.tサムネイルテクスチャを作成する( Path.GetDirectoryName( song.ScoreFile ) );
                        this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
		                this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
				    }
                    txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                }

				for( int j = 0; j < 3; j++ )
					this.stバー情報[ i ].nスキル値[ j ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ j ];

				song = this.r次の曲( song );
			}

			this.n現在の選択行 = 5;
		}
        private void tパスを指定してサムネイル画像を生成する( int nバー番号, string strDTXPath, Eバー種別 eType )
        {
            if( nバー番号 < 0 || nバー番号 > 12 )
                return;

            //try
			{
                //if ( this.stバー情報[ nバー番号 ].eバー種別 == Eバー種別.Score || this.stバー情報[ nバー番号 ].eバー種別 == Eバー種別.Box)
                {
                    if (!File.Exists(strDTXPath))
                    {
                        this.txTumbnail[nバー番号] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"), false);
                    }
                    else
                    {
                        this.txTumbnail[nバー番号] = CDTXMania.tテクスチャの生成(strDTXPath);
                    }
                }
                //else if( this.stバー情報[ nバー番号 ].eバー種別 == Eバー種別.Random)
                //{
                //    if( this.txジャケットランダム != null )
                //        this.txTumbnail[ nバー番号 ] = this.txジャケットランダム;
                //}
                //else if( this.stバー情報[ nバー番号 ].eバー種別 == Eバー種別.BackBox)
                //{
                //    if( this.txジャケットボックスクローズ != null )
                //        this.txTumbnail[ nバー番号 ] = this.txジャケットボックスクローズ;
                //}
			}
            /*
			catch( CTextureCreateFailedException )
			{
                if ( this.txジャケット指定が無い場合の画像 != null )
                {
                    this.txTumbnail[ nバー番号 ] = this.txジャケット指定が無い場合の画像;
                }
                else
                {
                    this.txTumbnail[ nバー番号 ] = null;
                }
			}
                 */
        }
        private CTexture tパスを指定してサムネイル画像を生成して返す( int nバー番号, string strDTXPath, Eバー種別 eType )
        {
            if (nバー番号 < 0 || nバー番号 > 12)
                return this.txTumbnail[nバー番号] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"), false);

            //try
			{
                //if (eType == Eバー種別.Score || eType == Eバー種別.Box)
                {
                    if (!File.Exists(strDTXPath))
                    {
                        return this.txTumbnail[nバー番号] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"), false);
                    }
                    else
                    {
                        return this.txTumbnail[nバー番号] = CDTXMania.tテクスチャの生成(strDTXPath);
                    }
                }
                //else if (eType == Eバー種別.Random)
                {
                //    if(this.txジャケットランダム != null)
                //        this.txTumbnail[nバー番号] = this.txジャケットランダム;
                }
                //else if (eType == Eバー種別.BackBox)
                {
                //    if(this.txジャケットボックスクローズ != null)
                //        this.txTumbnail[nバー番号] = this.txジャケットボックスクローズ;
                }
			}
            /*
			catch( CTextureCreateFailedException )
			{
                if ( this.txジャケット指定が無い場合の画像 != null )
                {
                    this.txTumbnail[ nバー番号 ] = this.txジャケット指定が無い場合の画像;
                }
                else
                {
                    this.txTumbnail[ nバー番号 ] = null;
                }
			}
                 */
        }
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            //2013.09.05.kairera0467 中央にしか使用することはないので、色は黒固定。
            //現在は機能しない(面倒なので実装してない)が、そのうち使用する予定。
            //PrivateFontの試験運転も兼ねて。
            //CPrivateFastFont
            prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 28, FontStyle.Regular );
            Bitmap bmp;
            
            bmp = prvFont.DrawPrivateFont( str文字, Color.Black, Color.Transparent );

            SizeF sz曲名;

            #region [ 曲名表示に必要となるサイズを取得する。]
            //-----------------
            using( var bmpDummy = new Bitmap(1, 1) )
            {
                var g = Graphics.FromImage( bmpDummy );
                g.PageUnit = GraphicsUnit.Pixel;
                sz曲名 = g.MeasureString( str文字, this.ft曲リスト用フォント);
                g.Dispose();
                //this.stバー情報[ nバー番号 ].nタイトル名テクスチャの長さdot = (int)g.MeasureString(str文字, this.ft曲リスト用フォント).Width;
            }
            //-----------------
            #endregion
            int n最大幅px = 200;
            int height = 25;
            int width = (int)((sz曲名.Width + 2) * 0.5f);
            if (width > (CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2))
                width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

            float f拡大率X = (width <= n最大幅px) ? 0.5f : (((float)n最大幅px / (float)width) * 0.5f);	// 長い文字列は横方向に圧縮。

            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );

            if( tx文字テクスチャ != null )
                tx文字テクスチャ.vc拡大縮小倍率 = new Vector3( f拡大率X, 0.5f, 1f );

            //prvFont.Dispose();
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private void tパネルの生成( int nバー番号, string str曲名, string strアーティスト名, Color color )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。
            
            if( nバー番号 < 0 || nバー番号 > 12 )
				return;
            try
            {
                Bitmap b4font;
                Bitmap bSongPanel;
                Image imgSongPanel;
                Image imgCustomSongNameTexture;
                Image imgCuttomArtistNameTexture;
                SizeF sz曲名;
                SizeF szアーティスト名;

                bool bFoundTitleTexture = false;
                bool bFoundArtistTexture = false;

                b4font = new Bitmap( 1, 1 );
                Graphics graphics = Graphics.FromImage( b4font );
                graphics.PageUnit = GraphicsUnit.Pixel;
                imgSongPanel = Image.FromFile( CSkin.Path( @"Graphics\5_music panel.png" ) );

                bSongPanel = new Bitmap( 223, 279 );

                graphics = Graphics.FromImage( bSongPanel );
                graphics.DrawImage( imgSongPanel, 0, 0, 223, 279 );

                string strPassBefore = "";
                string strPassAfter = "";
                try
                {
                    strPassBefore = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
                    strPassAfter = strPassBefore.Replace( this.stバー情報[ nバー番号 ].ar譜面情報.Preimage, "" );
                }
                catch
                {
                    //Replaceでエラーが出たらここで適切な処理ができるようにしたい。
                    strPassBefore = "";
                    strPassAfter = "";

                }
                    string strPath = ( strPassAfter + "TitleTexture.png" );
                    if( File.Exists( ( strPath ) ) )
                    {
                        imgCustomSongNameTexture = Image.FromFile( strPath );
                        graphics.DrawImage( imgCustomSongNameTexture, 4, -1, 223, 33 );
                        bFoundTitleTexture = true;
                    }
                    if( File.Exists( ( strPassAfter + "ArtistTexture.png" ) ) )
                    {
                        imgCuttomArtistNameTexture = Image.FromFile( strPassAfter + "ArtistTexture.png" );
                        graphics.DrawImage(imgCuttomArtistNameTexture, 0, 252, 223, 26);
                        bFoundArtistTexture = true;
                    }


				#region [ 曲名表示に必要となるサイズを取得する。]
				//-----------------
				using( var bmpDummy = new Bitmap( 1, 1 ) )
				{
					var g = Graphics.FromImage( bmpDummy );
					g.PageUnit = GraphicsUnit.Pixel;
					sz曲名 = g.MeasureString( str曲名, this.ft曲リスト用フォント );
                    szアーティスト名 = g.MeasureString( strアーティスト名, this.ft曲リスト用フォント );
                    this.stバー情報[ nバー番号 ].nタイトル名テクスチャの長さdot = (int) g.MeasureString( strアーティスト名, new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16 ) ).Width;
                    g.Dispose();
				}
				//-----------------
				#endregion

                int n最大幅px = 200;
				int height = 25;
				int width = (int) ( ( sz曲名.Width + 2 ) );
				if( width > ( CDTXMania.app.Device.Capabilities.MaxTextureWidth ) )
					width = CDTXMania.app.Device.Capabilities.MaxTextureWidth;	// 右端断ち切れ仕方ないよね

				float f拡大率X = ( width <= n最大幅px ) ? 1 : ( ( (float) n最大幅px / (float) width )  );	// 長い文字列は横方向に圧縮。

				using( var bmp = new Bitmap( width, height, PixelFormat.Format32bppArgb ) )		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
				using( var g = Graphics.FromImage( bmp ) )
				{
					graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					float y = ( ( ( float ) bmp.Height ) ) - ( ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot ) );
					//graphics.DrawString( str曲名, this.ft曲リスト用フォント, new SolidBrush( this.color文字影 ), (float) 2f, (float) ( y + 2f ) );
                    if( bFoundTitleTexture == false )
					    graphics.DrawString( str曲名, new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16 ), new SolidBrush( color ), 5f, y - 2 );
                    if( bFoundArtistTexture == false )
                        graphics.DrawString( strアーティスト名, new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16 ), new SolidBrush( Color.White ), (float)218f - this.stバー情報[ nバー番号 ].nタイトル名テクスチャの長さdot, 255f);
                    //graphics.DrawString( strアーティスト名, this.ft曲リスト用フォント, new SolidBrush( Color.White ), 234f, 258f );

					CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txパネル );

                    this.stバー情報[ nバー番号 ].txパネル = new CTexture( CDTXMania.app.Device, bSongPanel, CDTXMania.TextureFormat, false );

                    bmp.Dispose();
                    g.Dispose();
				}

                b4font.Dispose();
                bSongPanel.Dispose();
                imgSongPanel.Dispose();
                graphics.Dispose();
            }
            catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", str曲名 );
				this.stバー情報[ nバー番号 ].txパネル = null;
			}
            
        }
        
        private CTexture tカスタム曲名の生成( int nバー番号 )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。

            Bitmap b4font;
            Bitmap bCustomSongNameTexture;
            Image imgCustomSongNameTexture;

            b4font = new Bitmap( 1, 1 );
            Graphics graphicsA = Graphics.FromImage( b4font );

            graphicsA.PageUnit = GraphicsUnit.Pixel;

            imgCustomSongNameTexture = Image.FromFile( CSkin.Path( @"Graphics\7_Dummy.png" ) );

            
            bCustomSongNameTexture = new Bitmap( 240, 40 );

            graphicsA = Graphics.FromImage( bCustomSongNameTexture );
            graphicsA.DrawImage( bCustomSongNameTexture, 0, 0, 180, 25 );


            string strPassAfter = "";
            try
            {
                strPassAfter = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
            }
            catch
            {
                strPassAfter = "";
            }

            string strPath = ( strPassAfter + "TitleTexture.png" );
            if( File.Exists( ( strPath ) ) )
            {
                imgCustomSongNameTexture = Image.FromFile( strPath );
                imgCustomSongNameTexture = this.CreateNegativeImage( imgCustomSongNameTexture );
                graphicsA.DrawImage( imgCustomSongNameTexture, 6, 1, 180, 25 );
                imgCustomSongNameTexture.Dispose();
            }


			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタム曲名テクスチャ );
			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタムアーティスト名テクスチャ );
            CTexture txTex = new CTexture( CDTXMania.app.Device, bCustomSongNameTexture, CDTXMania.TextureFormat, false );

            b4font.Dispose();
            graphicsA.Dispose();
            bCustomSongNameTexture.Dispose();

            return txTex;
        }
        private CTexture tカスタムアーティスト名テクスチャの生成( int nバー番号 )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。

            Bitmap b4font;
            Bitmap bCustomArtistNameTexture;
            Image imgCustomArtistNameTexture;

            b4font = new Bitmap( 1, 1 );
            Graphics graphics = Graphics.FromImage( b4font );

            graphics.PageUnit = GraphicsUnit.Pixel;

            imgCustomArtistNameTexture = Image.FromFile( CSkin.Path( @"Graphics\7_Dummy.png" ) );

            
            bCustomArtistNameTexture = new Bitmap( 210, 27 );

            graphics = Graphics.FromImage( bCustomArtistNameTexture );
            graphics.DrawImage( bCustomArtistNameTexture, 0, 0, 196, 27 );


            string strPassAfter = "";
            try
            {
                strPassAfter = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
            }
            catch
            {
                strPassAfter = "";
            }

            string strPath = (strPassAfter + "ArtistTexture.png");
            if (File.Exists((strPath)))
            {
                imgCustomArtistNameTexture = Image.FromFile(strPath);
                imgCustomArtistNameTexture = this.CreateNegativeImage( imgCustomArtistNameTexture );
                graphics.DrawImage(imgCustomArtistNameTexture, 16, 2, 196, 27);
            }

			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタム曲名テクスチャ );
			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタムアーティスト名テクスチャ );
            this.stバー情報[nバー番号].nアーティスト名テクスチャの長さdot = 210;
            CTexture txTex = new CTexture( CDTXMania.app.Device, bCustomArtistNameTexture, CDTXMania.TextureFormat, false );
            return txTex;
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
                    this.stバー情報[ nバー番号 ].nタイトル名テクスチャの長さdot = (int) g.MeasureString( str曲名, this.ft曲リスト用フォント ).Width;
				}
				//-----------------
				#endregion

				int n最大幅px = 200;
				int height = 25;
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

					this.stバー情報[ nバー番号 ].txタイトル名 = new CTexture( CDTXMania.app.Device, bmp, CDTXMania.TextureFormat, false );
					this.stバー情報[ nバー番号 ].txタイトル名.vc拡大縮小倍率 = new Vector3( f拡大率X, 0.5f, 1f );
				}
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", str曲名 );
				this.stバー情報[ nバー番号 ].txタイトル名 = null;
			}
		}
        private void tアーティスト名テクスチャの生成( int nバー番号, string strアーティスト名 )
		{
			if( nバー番号 < 0 || nバー番号 > 12 )
				return;

			try
			{
				SizeF szアーティスト名;

				#region [ 曲名表示に必要となるサイズを取得する。]
				//-----------------
				using( var bmpDummy = new Bitmap( 1, 1 ) )
				{
					var g = Graphics.FromImage( bmpDummy );
					g.PageUnit = GraphicsUnit.Pixel;
					szアーティスト名 = g.MeasureString( strアーティスト名, this.ft曲リスト用フォント );
				}
				//-----------------
				#endregion

				int n最大幅px = 210;
				int height = 25;
				int width = (int) ( ( szアーティスト名.Width + 2 ) * 0.5f );
				if( width > ( CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2 ) )
					width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

				float f拡大率X = ( width <= n最大幅px ) ? 0.5f : ( ( (float) n最大幅px / (float) width ) * 0.5f );	// 長い文字列は横方向に圧縮。

				using( var bmp = new Bitmap( width * 2, height * 2, PixelFormat.Format32bppArgb ) )		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
				using( var g = Graphics.FromImage( bmp ) )
				{
					g.TextRenderingHint = TextRenderingHint.AntiAlias;
					float y = ( ( ( float ) bmp.Height ) / 2f ) - ( ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2f ) / 2f );
                    g.DrawString( strアーティスト名, this.ft曲リスト用フォント, new SolidBrush( this.color文字影 ), (float)2f, (float)(y + 2f));
                    g.DrawString( strアーティスト名, this.ft曲リスト用フォント, new SolidBrush( Color.White ), 0f, y );

					CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txアーティスト名 );
                    this.stバー情報[ nバー番号 ].nアーティスト名テクスチャの長さdot = (int)((g.MeasureString(strアーティスト名, this.ft曲リスト用フォント).Width) * f拡大率X);
					this.stバー情報[ nバー番号 ].txアーティスト名 = new CTexture( CDTXMania.app.Device, bmp, CDTXMania.TextureFormat, false );
					this.stバー情報[ nバー番号 ].txアーティスト名.vc拡大縮小倍率 = new Vector3( f拡大率X, 0.5f, 1f );
				}
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", strアーティスト名 );
				this.stバー情報[ nバー番号 ].txアーティスト名 = null;
			}
		}
        private void tアイテム数の描画()
        {
            string s = nCurrentPosition.ToString() + "/" + nNumOfItems.ToString();
            int x = 1150;
            int y = 200;

            for (int p = s.Length - 1; p >= 0; p--)
            {
                tアイテム数の描画・１桁描画(x, y, s[p]);
                x -= 16;
            }
        }
        private void tアイテム数の描画・１桁描画(int x, int y, char s数値)
        {
            int dx, dy;
            if (s数値 == '/')
            {
                dx = 96;
                dy = 0;
            }
            else
            {
                int n = (int)s数値 - (int)'0';
                dx = (n % 6) * 16;
                dy = (n / 6) * 16;
            }
            if (this.txアイテム数数字 != null)
            {
                this.txアイテム数数字.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(dx, dy, 16, 16));
            }
        }

        
        //DOBON.NETから拝借。
        //http://dobon.net/vb/dotnet/graphics/drawnegativeimage.html
        /// <summary>
        /// 指定された画像からネガティブイメージを作成する
        /// </summary>
        /// <param name="img">基の画像</param>
        /// <returns>作成されたネガティブイメージ</returns>
        public Image CreateNegativeImage(Image img)
        {
            //ネガティブイメージの描画先となるImageオブジェクトを作成
            Bitmap negaImg = new Bitmap(img.Width, img.Height);
            //negaImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage(negaImg);

            //ColorMatrixオブジェクトの作成
            System.Drawing.Imaging.ColorMatrix cm =
                new System.Drawing.Imaging.ColorMatrix();
            //ColorMatrixの行列の値を変更して、色が反転されるようにする
            cm.Matrix00 = -1;
            cm.Matrix11 = -1;
            cm.Matrix22 = -1;
            cm.Matrix33 = 1;
            cm.Matrix40 = cm.Matrix41 = cm.Matrix42 = cm.Matrix44 = 1;

            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用して色が反転した画像を描画
            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            g.Dispose();

            return negaImg;
        }
        //-----------------
		#endregion
	}
}
