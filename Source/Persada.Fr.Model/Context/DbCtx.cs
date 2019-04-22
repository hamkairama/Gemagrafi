namespace Persada.Fr.Model.Master
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbCtx : DbContext
    {
        public DbCtx() : base("name=DbConextion") { }

        public DbSet<RegisterUser> RegisterUser { get; set; }
        public DbSet<ClientKey> ClientKeys { get; set; }
        public DbSet<TokensManager> TokensManager { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }



        public virtual DbSet<GEMA_TM_CATEGORY> GEMA_TM_CATEGORY { get; set; }
        public virtual DbSet<GEMA_TM_CONTACT_US> GEMA_TM_CONTACT_US { get; set; }
        public virtual DbSet<GEMA_TM_NEWS> GEMA_TM_NEWS { get; set; }
        public virtual DbSet<GEMA_TM_PARAMETER> GEMA_TM_PARAMETER { get; set; }
        public virtual DbSet<GEMA_TM_PRICEL_LIST> GEMA_TM_PRICEL_LIST { get; set; }
        public virtual DbSet<GEMA_TM_PROMOTION> GEMA_TM_PROMOTION { get; set; }
        public virtual DbSet<GEMA_TM_SLIDESHOW> GEMA_TM_SLIDESHOW { get; set; }
        public virtual DbSet<GEMA_TM_SUB_CATEGORY> GEMA_TM_SUB_CATEGORY { get; set; }
        public virtual DbSet<GEMA_TM_TRANSACTION_COUNT> GEMA_TM_TRANSACTION_COUNT { get; set; }
        public virtual DbSet<GEMA_TM_USER> GEMA_TM_USER { get; set; }
        public virtual DbSet<GEMA_TM_USER_PROFILE> GEMA_TM_USER_PROFILE { get; set; }
        public virtual DbSet<GEMA_TM_USER_PROFILE_SOSMED> GEMA_TM_USER_PROFILE_SOSMED { get; set; }
        public virtual DbSet<GEMA_TR_BOOKING> GEMA_TR_BOOKING { get; set; }
        public virtual DbSet<GEMA_TR_BOOKING_DETAIL> GEMA_TR_BOOKING_DETAIL { get; set; }
        public virtual DbSet<GEMA_TM_SOSMED> GEMA_TM_SOSMED { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.IMAGE_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.CLASS)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CATEGORY>()
                .HasMany(e => e.GEMA_TM_SUB_CATEGORY)
                .WithRequired(e => e.GEMA_TM_CATEGORY)
                .HasForeignKey(e => e.CATEGORY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.NAME_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.EMAIL_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.PHONE_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_CONTACT_US>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_NEWS>()
                .Property(e => e.TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_NEWS>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_NEWS>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_NEWS>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PARAMETER>()
                .Property(e => e.PARAMETER_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PARAMETER>()
                .Property(e => e.PARAMETER_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PARAMETER>()
                .Property(e => e.PARAMETER_VALUE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PARAMETER>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PARAMETER>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .Property(e => e.IMAGE_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PRICEL_LIST>()
                .HasMany(e => e.GEMA_TR_BOOKING_DETAIL)
                .WithRequired(e => e.GEMA_TM_PRICEL_LIST)
                .HasForeignKey(e => e.PRICE_LIST_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TM_PROMOTION>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PROMOTION>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_PROMOTION>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.TITTLE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.CONTENT_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.CLASS)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.PHOTO_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SLIDESHOW>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.ICON)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.DATA_EMBED)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.DATA_WIDGET_ID)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SOSMED>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SUB_CATEGORY>()
                .Property(e => e.SUB_CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SUB_CATEGORY>()
                .Property(e => e.IMAGE_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SUB_CATEGORY>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SUB_CATEGORY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_SUB_CATEGORY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_TRANSACTION_COUNT>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_TRANSACTION_COUNT>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.USER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.USER_MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.USER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .HasMany(e => e.GEMA_TM_USER_PROFILE)
                .WithRequired(e => e.GEMA_TM_USER)
                .HasForeignKey(e => e.USER_ID_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TM_USER>()
                .HasMany(e => e.GEMA_TR_BOOKING)
                .WithRequired(e => e.GEMA_TM_USER)
                .HasForeignKey(e => e.USER_ID_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.PHOTO_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.GENDER)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.JOB)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.COMPANY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.HOBBY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE>()
                .HasMany(e => e.GEMA_TM_USER_PROFILE_SOSMED)
                .WithRequired(e => e.GEMA_TM_USER_PROFILE)
                .HasForeignKey(e => e.USER_PROFILE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.SOSMED_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.SOSMED_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.PAYMENT_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.DISCOUNT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.TOTAL_PAYMENT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.GRAND_TOTAL_PAYMENT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.RECEIVE_PAYMENT_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.COMPLETE_PAYMENT_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.PAYMENT_IMAGE_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING>()
                .HasMany(e => e.GEMA_TR_BOOKING_DETAIL)
                .WithRequired(e => e.GEMA_TR_BOOKING)
                .HasForeignKey(e => e.BOOKING_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GEMA_TR_BOOKING_DETAIL>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<GEMA_TR_BOOKING_DETAIL>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);
        }
    }
}
