using System;
using System.Collections.Generic;
using System.Text;

namespace FDK
{
	/// <summary>
	/// <para>タイマの抽象クラス。</para>
	/// <para>このクラスを継承し、override したクラスを作成することで、任意のクロックを持つタイマを作成できる。</para>
	/// </summary>
	public abstract class CTimerBase : IDisposable
	{
		public const long n未使用 = -1;

		// この２つを override する。
		public abstract long nシステム時刻ms
		{
			get;
		}
		public abstract void Dispose();

		public long n現在時刻ms
		{
			get
			{
				if( this.n停止数 > 0 )
					return ( this.n一時停止システム時刻ms - this.n前回リセットした時のシステム時刻ms );

				return ( this.n更新システム時刻ms - this.n前回リセットした時のシステム時刻ms );
			}
			set
			{
				if( this.n停止数 > 0 )
					this.n前回リセットした時のシステム時刻ms = this.n一時停止システム時刻ms - value;
				else
					this.n前回リセットした時のシステム時刻ms = this.n更新システム時刻ms - value;
			}
		}
		public long nリアルタイム現在時刻ms
		{
			get
			{
				if( this.n停止数 > 0 )
					return ( this.n一時停止システム時刻ms - this.n前回リセットした時のシステム時刻ms );

				return ( this.nシステム時刻ms - this.n前回リセットした時のシステム時刻ms );
			}
		}
		public long n前回リセットした時のシステム時刻ms
		{
			get;
			protected set;
		}

		public void tリセット()
		{
			this.t更新();
			this.n前回リセットした時のシステム時刻ms = this.n更新システム時刻ms;
			this.n一時停止システム時刻ms = this.n更新システム時刻ms;
			this.n停止数 = 0;
		}
		public void t一時停止()
		{
			if( this.n停止数 == 0 )
				this.n一時停止システム時刻ms = this.n更新システム時刻ms;

			this.n停止数++;
		}
		public void t更新()
		{
			this.n更新システム時刻ms = this.nシステム時刻ms;
		}
		public void t再開()
		{
			if( this.n停止数 > 0 )
			{
				this.n停止数--;
				if( this.n停止数 == 0 )
				{
					this.t更新();
					this.n前回リセットした時のシステム時刻ms += this.n更新システム時刻ms - this.n一時停止システム時刻ms;
				}
			}
		}
		
		#region [ protected ]
		//-----------------
		protected long n一時停止システム時刻ms = 0;
		protected long n更新システム時刻ms = 0;
		protected int n停止数 = 0;
		//-----------------
		#endregion
	}
}
