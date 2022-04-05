.netframework
bll -- class library
dal -- class library
entity -- class library

references
uý  
	-bll
	-entity
bll
	-dal
	-entity
dal
	-entity

entity i bütün katamanlar görür.

uý -istek atmak için
	istekler bll tarafýndan karþýlanýr.

ýdentityModel 
class ekle
public yap





--BLL 
repository, account ve setting klasörlerini açýlýyor.
repository---- new class RepositoryBase --> public abstract
public abstract class RepositoryBase<T,Id> where T:class,new()
	-30 tane tablo varsa30 tane yazmamýz gerekirdi. Bu sayede hepsini kapsayan generic yapýsýný yazdýk.
	-- kim geliyorsa ýd ile gelsin. new'lenebilen class haricinde kimse alýnamaz.
protected static MyContext dbContext; 
	-- kalýtým ile gittiði yerlerde rahatlýkla kullanýlabilir.
public virtual -- abstract class ta virtual olursa override edebilir.

dbContext = new MyContext();
                return dbContext.Set<T>().ToList();

hepsini getir.

paralelde giden görev --- async

public virtual T GetById(Id id)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();// dbContext nesnesi null mý? Null deðilse onu kullan null ise yeni oluþtur.
                return dbContext.Set<T>().Find(id);//id bul.
			}

			public async Task<T>....
				return await
				savechangesasync


 public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
            dbContext = null;
        }
	connection baðlntýsýný koparmak yok etmek


