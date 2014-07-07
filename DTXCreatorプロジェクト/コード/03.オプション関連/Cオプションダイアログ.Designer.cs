﻿namespace DTXCreator.オプション関連
{
	partial class Cオプションダイアログ
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Cオプションダイアログ ) );
			this.tabPage全般 = new System.Windows.Forms.TabPage();
			this.checkBoxPlaySoundOnChip = new System.Windows.Forms.CheckBox();
			this.checkBoxPreviewBGM = new System.Windows.Forms.CheckBox();
			this.checkBoxオートフォーカス = new System.Windows.Forms.CheckBox();
			this.label個まで表示する = new System.Windows.Forms.Label();
			this.checkBox最近使用したファイル = new System.Windows.Forms.CheckBox();
			this.numericUpDown最近使用したファイルの最大表示個数 = new System.Windows.Forms.NumericUpDown();
			this.tabControlオプション = new System.Windows.Forms.TabControl();
			this.tabPageLanes = new System.Windows.Forms.TabPage();
			this.labelSelectLanes = new System.Windows.Forms.Label();
			this.checkedListBoxLaneSelectList = new System.Windows.Forms.CheckedListBox();
			this.tabPageViewer = new System.Windows.Forms.TabPage();
			this.groupBox_SelectViewer = new System.Windows.Forms.GroupBox();
			this.groupBox_DTXManiaSettings = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox_TimeStretch = new System.Windows.Forms.CheckBox();
			this.checkBox_VSyncWait = new System.Windows.Forms.CheckBox();
			this.checkBox_GRmode = new System.Windows.Forms.CheckBox();
			this.groupBox_SoundDeviceSettings = new System.Windows.Forms.GroupBox();
			this.label_Notice = new System.Windows.Forms.Label();
			this.radioButton_DirectSound = new System.Windows.Forms.RadioButton();
			this.radioButton_WASAPI = new System.Windows.Forms.RadioButton();
			this.comboBox_ASIOdevices = new System.Windows.Forms.ComboBox();
			this.radioButton_ASIO = new System.Windows.Forms.RadioButton();
			this.radioButton_UseDTXViewer = new System.Windows.Forms.RadioButton();
			this.radioButton_UseDTXManiaGR = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tabPage全般.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.numericUpDown最近使用したファイルの最大表示個数 ) ).BeginInit();
			this.tabControlオプション.SuspendLayout();
			this.tabPageLanes.SuspendLayout();
			this.tabPageViewer.SuspendLayout();
			this.groupBox_SelectViewer.SuspendLayout();
			this.groupBox_DTXManiaSettings.SuspendLayout();
			this.groupBox_SoundDeviceSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPage全般
			// 
			this.tabPage全般.Controls.Add( this.checkBoxPlaySoundOnChip );
			this.tabPage全般.Controls.Add( this.checkBoxPreviewBGM );
			this.tabPage全般.Controls.Add( this.checkBoxオートフォーカス );
			this.tabPage全般.Controls.Add( this.label個まで表示する );
			this.tabPage全般.Controls.Add( this.checkBox最近使用したファイル );
			this.tabPage全般.Controls.Add( this.numericUpDown最近使用したファイルの最大表示個数 );
			resources.ApplyResources( this.tabPage全般, "tabPage全般" );
			this.tabPage全般.Name = "tabPage全般";
			this.tabPage全般.UseVisualStyleBackColor = true;
			// 
			// checkBoxPlaySoundOnChip
			// 
			resources.ApplyResources( this.checkBoxPlaySoundOnChip, "checkBoxPlaySoundOnChip" );
			this.checkBoxPlaySoundOnChip.Name = "checkBoxPlaySoundOnChip";
			this.checkBoxPlaySoundOnChip.UseVisualStyleBackColor = true;
			// 
			// checkBoxPreviewBGM
			// 
			resources.ApplyResources( this.checkBoxPreviewBGM, "checkBoxPreviewBGM" );
			this.checkBoxPreviewBGM.Name = "checkBoxPreviewBGM";
			this.checkBoxPreviewBGM.UseVisualStyleBackColor = true;
			// 
			// checkBoxオートフォーカス
			// 
			resources.ApplyResources( this.checkBoxオートフォーカス, "checkBoxオートフォーカス" );
			this.checkBoxオートフォーカス.Name = "checkBoxオートフォーカス";
			this.checkBoxオートフォーカス.UseVisualStyleBackColor = true;
			// 
			// label個まで表示する
			// 
			resources.ApplyResources( this.label個まで表示する, "label個まで表示する" );
			this.label個まで表示する.Name = "label個まで表示する";
			// 
			// checkBox最近使用したファイル
			// 
			resources.ApplyResources( this.checkBox最近使用したファイル, "checkBox最近使用したファイル" );
			this.checkBox最近使用したファイル.Name = "checkBox最近使用したファイル";
			this.checkBox最近使用したファイル.UseVisualStyleBackColor = true;
			// 
			// numericUpDown最近使用したファイルの最大表示個数
			// 
			resources.ApplyResources( this.numericUpDown最近使用したファイルの最大表示個数, "numericUpDown最近使用したファイルの最大表示個数" );
			this.numericUpDown最近使用したファイルの最大表示個数.Maximum = new decimal( new int[] {
            10,
            0,
            0,
            0} );
			this.numericUpDown最近使用したファイルの最大表示個数.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.numericUpDown最近使用したファイルの最大表示個数.Name = "numericUpDown最近使用したファイルの最大表示個数";
			this.numericUpDown最近使用したファイルの最大表示個数.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			// 
			// tabControlオプション
			// 
			resources.ApplyResources( this.tabControlオプション, "tabControlオプション" );
			this.tabControlオプション.Controls.Add( this.tabPage全般 );
			this.tabControlオプション.Controls.Add( this.tabPageLanes );
			this.tabControlオプション.Controls.Add( this.tabPageViewer );
			this.tabControlオプション.Name = "tabControlオプション";
			this.tabControlオプション.SelectedIndex = 0;
			this.tabControlオプション.KeyDown += new System.Windows.Forms.KeyEventHandler( this.tabControlオプション_KeyDown );
			// 
			// tabPageLanes
			// 
			this.tabPageLanes.Controls.Add( this.labelSelectLanes );
			this.tabPageLanes.Controls.Add( this.checkedListBoxLaneSelectList );
			resources.ApplyResources( this.tabPageLanes, "tabPageLanes" );
			this.tabPageLanes.Name = "tabPageLanes";
			this.tabPageLanes.UseVisualStyleBackColor = true;
			// 
			// labelSelectLanes
			// 
			resources.ApplyResources( this.labelSelectLanes, "labelSelectLanes" );
			this.labelSelectLanes.Name = "labelSelectLanes";
			// 
			// checkedListBoxLaneSelectList
			// 
			this.checkedListBoxLaneSelectList.CheckOnClick = true;
			this.checkedListBoxLaneSelectList.FormattingEnabled = true;
			resources.ApplyResources( this.checkedListBoxLaneSelectList, "checkedListBoxLaneSelectList" );
			this.checkedListBoxLaneSelectList.Name = "checkedListBoxLaneSelectList";
			// 
			// tabPageViewer
			// 
			this.tabPageViewer.Controls.Add( this.groupBox_SelectViewer );
			resources.ApplyResources( this.tabPageViewer, "tabPageViewer" );
			this.tabPageViewer.Name = "tabPageViewer";
			this.tabPageViewer.UseVisualStyleBackColor = true;
			// 
			// groupBox_SelectViewer
			// 
			this.groupBox_SelectViewer.Controls.Add( this.groupBox_DTXManiaSettings );
			this.groupBox_SelectViewer.Controls.Add( this.groupBox_SoundDeviceSettings );
			this.groupBox_SelectViewer.Controls.Add( this.radioButton_UseDTXViewer );
			this.groupBox_SelectViewer.Controls.Add( this.radioButton_UseDTXManiaGR );
			resources.ApplyResources( this.groupBox_SelectViewer, "groupBox_SelectViewer" );
			this.groupBox_SelectViewer.Name = "groupBox_SelectViewer";
			this.groupBox_SelectViewer.TabStop = false;
			// 
			// groupBox_DTXManiaSettings
			// 
			this.groupBox_DTXManiaSettings.Controls.Add( this.label1 );
			this.groupBox_DTXManiaSettings.Controls.Add( this.checkBox_TimeStretch );
			this.groupBox_DTXManiaSettings.Controls.Add( this.checkBox_VSyncWait );
			this.groupBox_DTXManiaSettings.Controls.Add( this.checkBox_GRmode );
			resources.ApplyResources( this.groupBox_DTXManiaSettings, "groupBox_DTXManiaSettings" );
			this.groupBox_DTXManiaSettings.Name = "groupBox_DTXManiaSettings";
			this.groupBox_DTXManiaSettings.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources( this.label1, "label1" );
			this.label1.Name = "label1";
			// 
			// checkBox_TimeStretch
			// 
			resources.ApplyResources( this.checkBox_TimeStretch, "checkBox_TimeStretch" );
			this.checkBox_TimeStretch.Name = "checkBox_TimeStretch";
			this.checkBox_TimeStretch.UseVisualStyleBackColor = true;
			// 
			// checkBox_VSyncWait
			// 
			resources.ApplyResources( this.checkBox_VSyncWait, "checkBox_VSyncWait" );
			this.checkBox_VSyncWait.Name = "checkBox_VSyncWait";
			this.checkBox_VSyncWait.UseVisualStyleBackColor = true;
			// 
			// checkBox_GRmode
			// 
			resources.ApplyResources( this.checkBox_GRmode, "checkBox_GRmode" );
			this.checkBox_GRmode.Name = "checkBox_GRmode";
			this.checkBox_GRmode.UseVisualStyleBackColor = true;
			// 
			// groupBox_SoundDeviceSettings
			// 
			this.groupBox_SoundDeviceSettings.Controls.Add( this.label_Notice );
			this.groupBox_SoundDeviceSettings.Controls.Add( this.radioButton_DirectSound );
			this.groupBox_SoundDeviceSettings.Controls.Add( this.radioButton_WASAPI );
			this.groupBox_SoundDeviceSettings.Controls.Add( this.comboBox_ASIOdevices );
			this.groupBox_SoundDeviceSettings.Controls.Add( this.radioButton_ASIO );
			resources.ApplyResources( this.groupBox_SoundDeviceSettings, "groupBox_SoundDeviceSettings" );
			this.groupBox_SoundDeviceSettings.Name = "groupBox_SoundDeviceSettings";
			this.groupBox_SoundDeviceSettings.TabStop = false;
			// 
			// label_Notice
			// 
			resources.ApplyResources( this.label_Notice, "label_Notice" );
			this.label_Notice.AutoEllipsis = true;
			this.label_Notice.Name = "label_Notice";
			// 
			// radioButton_DirectSound
			// 
			resources.ApplyResources( this.radioButton_DirectSound, "radioButton_DirectSound" );
			this.radioButton_DirectSound.Name = "radioButton_DirectSound";
			this.radioButton_DirectSound.TabStop = true;
			this.radioButton_DirectSound.UseVisualStyleBackColor = true;
			this.radioButton_DirectSound.CheckedChanged += new System.EventHandler( this.radioButton_DirectSound_CheckedChanged );
			// 
			// radioButton_WASAPI
			// 
			resources.ApplyResources( this.radioButton_WASAPI, "radioButton_WASAPI" );
			this.radioButton_WASAPI.Name = "radioButton_WASAPI";
			this.radioButton_WASAPI.TabStop = true;
			this.radioButton_WASAPI.UseVisualStyleBackColor = true;
			this.radioButton_WASAPI.CheckedChanged += new System.EventHandler( this.radioButton_WASAPI_CheckedChanged );
			// 
			// comboBox_ASIOdevices
			// 
			this.comboBox_ASIOdevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_ASIOdevices.FormattingEnabled = true;
			resources.ApplyResources( this.comboBox_ASIOdevices, "comboBox_ASIOdevices" );
			this.comboBox_ASIOdevices.Name = "comboBox_ASIOdevices";
			// 
			// radioButton_ASIO
			// 
			resources.ApplyResources( this.radioButton_ASIO, "radioButton_ASIO" );
			this.radioButton_ASIO.Name = "radioButton_ASIO";
			this.radioButton_ASIO.TabStop = true;
			this.radioButton_ASIO.UseVisualStyleBackColor = true;
			this.radioButton_ASIO.CheckedChanged += new System.EventHandler( this.radioButton_ASIO_CheckedChanged );
			// 
			// radioButton_UseDTXViewer
			// 
			resources.ApplyResources( this.radioButton_UseDTXViewer, "radioButton_UseDTXViewer" );
			this.radioButton_UseDTXViewer.Name = "radioButton_UseDTXViewer";
			this.radioButton_UseDTXViewer.TabStop = true;
			this.radioButton_UseDTXViewer.UseVisualStyleBackColor = true;
			this.radioButton_UseDTXViewer.CheckedChanged += new System.EventHandler( this.radioButton_UseDTXViewer_CheckedChanged );
			// 
			// radioButton_UseDTXManiaGR
			// 
			resources.ApplyResources( this.radioButton_UseDTXManiaGR, "radioButton_UseDTXManiaGR" );
			this.radioButton_UseDTXManiaGR.Name = "radioButton_UseDTXManiaGR";
			this.radioButton_UseDTXManiaGR.TabStop = true;
			this.radioButton_UseDTXManiaGR.UseVisualStyleBackColor = true;
			this.radioButton_UseDTXManiaGR.CheckedChanged += new System.EventHandler( this.radioButton_UseDTXManiaGR_CheckedChanged );
			// 
			// button1
			// 
			resources.ApplyResources( this.button1, "button1" );
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			resources.ApplyResources( this.buttonOK, "buttonOK" );
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// Cオプションダイアログ
			// 
			resources.ApplyResources( this, "$this" );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add( this.buttonOK );
			this.Controls.Add( this.button1 );
			this.Controls.Add( this.tabControlオプション );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cオプションダイアログ";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.Cオプションダイアログ_KeyDown );
			this.tabPage全般.ResumeLayout( false );
			this.tabPage全般.PerformLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.numericUpDown最近使用したファイルの最大表示個数 ) ).EndInit();
			this.tabControlオプション.ResumeLayout( false );
			this.tabPageLanes.ResumeLayout( false );
			this.tabPageLanes.PerformLayout();
			this.tabPageViewer.ResumeLayout( false );
			this.groupBox_SelectViewer.ResumeLayout( false );
			this.groupBox_SelectViewer.PerformLayout();
			this.groupBox_DTXManiaSettings.ResumeLayout( false );
			this.groupBox_DTXManiaSettings.PerformLayout();
			this.groupBox_SoundDeviceSettings.ResumeLayout( false );
			this.groupBox_SoundDeviceSettings.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TabPage tabPage全般;
		internal System.Windows.Forms.CheckBox checkBoxオートフォーカス;
		private System.Windows.Forms.Label label個まで表示する;
		internal System.Windows.Forms.CheckBox checkBox最近使用したファイル;
		internal System.Windows.Forms.NumericUpDown numericUpDown最近使用したファイルの最大表示個数;
		private System.Windows.Forms.TabControl tabControlオプション;
		internal System.Windows.Forms.CheckBox checkBoxPreviewBGM;
		internal System.Windows.Forms.CheckBox checkBoxPlaySoundOnChip;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TabPage tabPageLanes;
		internal System.Windows.Forms.CheckedListBox checkedListBoxLaneSelectList;
		private System.Windows.Forms.Label labelSelectLanes;
		private System.Windows.Forms.TabPage tabPageViewer;
		internal System.Windows.Forms.RadioButton radioButton_ASIO;
		internal System.Windows.Forms.RadioButton radioButton_WASAPI;
		internal System.Windows.Forms.RadioButton radioButton_DirectSound;
		internal System.Windows.Forms.RadioButton radioButton_UseDTXManiaGR;
		internal System.Windows.Forms.RadioButton radioButton_UseDTXViewer;
		private System.Windows.Forms.GroupBox groupBox_SelectViewer;
		internal System.Windows.Forms.ComboBox comboBox_ASIOdevices;
		private System.Windows.Forms.Label label_Notice;
		internal System.Windows.Forms.GroupBox groupBox_SoundDeviceSettings;
		private System.Windows.Forms.GroupBox groupBox_DTXManiaSettings;
		public System.Windows.Forms.CheckBox checkBox_GRmode;
		public System.Windows.Forms.CheckBox checkBox_TimeStretch;
		public System.Windows.Forms.CheckBox checkBox_VSyncWait;
		private System.Windows.Forms.Label label1;

	}
}