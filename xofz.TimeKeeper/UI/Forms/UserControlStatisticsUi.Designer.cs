namespace xofz.TimeKeeper.UI.Forms
{
    partial class UserControlStatisticsUi
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.MonthCalendar();
            this.startDatePicker = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeWorkedLabel = new System.Windows.Forms.Label();
            this.avgDailyLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "End:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Start:";
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(233, 34);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 5;
            this.endDatePicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.endDatePicker_DateChanged);
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(0, 34);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 4;
            this.startDatePicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.startDatePicker_DateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(472, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 37);
            this.label1.TabIndex = 8;
            this.label1.Text = "Statistics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(476, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Time worked:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeWorkedLabel
            // 
            this.timeWorkedLabel.AutoSize = true;
            this.timeWorkedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeWorkedLabel.Location = new System.Drawing.Point(584, 37);
            this.timeWorkedLabel.Name = "timeWorkedLabel";
            this.timeWorkedLabel.Size = new System.Drawing.Size(0, 25);
            this.timeWorkedLabel.TabIndex = 10;
            // 
            // avgDailyLabel
            // 
            this.avgDailyLabel.AutoSize = true;
            this.avgDailyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgDailyLabel.Location = new System.Drawing.Point(584, 73);
            this.avgDailyLabel.Name = "avgDailyLabel";
            this.avgDailyLabel.Size = new System.Drawing.Size(0, 25);
            this.avgDailyLabel.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(469, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 45);
            this.label6.TabIndex = 11;
            this.label6.Text = "Average daily time worked:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserControlStatisticsUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.avgDailyLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timeWorkedLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Name = "UserControlStatisticsUi";
            this.Size = new System.Drawing.Size(784, 298);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MonthCalendar endDatePicker;
        private System.Windows.Forms.MonthCalendar startDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeWorkedLabel;
        private System.Windows.Forms.Label avgDailyLabel;
        private System.Windows.Forms.Label label6;
    }
}
