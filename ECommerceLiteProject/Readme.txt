.netframework
bll -- class library
dal -- class library
entity -- class library

references
u�  
	-bll
	-entity
bll
	-dal
	-entity
dal
	-entity

entity i b�t�n katamanlar g�r�r.

u� -istek atmak i�in
	istekler bll taraf�ndan kar��lan�r.

�dentityModel 
class ekle
public yap





--BLL 
repository, account ve setting klas�rlerini a��l�yor.
repository---- new class RepositoryBase --> public abstract
public abstract class RepositoryBase<T,Id> where T:class,new()
	-30 tane tablo varsa30 tane yazmam�z gerekirdi. Bu sayede hepsini kapsayan generic yap�s�n� yazd�k.
	-- kim geliyorsa �d ile gelsin. new'lenebilen class haricinde kimse al�namaz.
protected static MyContext dbContext; 
	-- kal�t�m ile gitti�i yerlerde rahatl�kla kullan�labilir.
public virtual -- abstract class ta virtual olursa override edebilir.

dbContext = new MyContext();
                return dbContext.Set<T>().ToList();

hepsini getir.

paralelde giden g�rev --- async

public virtual T GetById(Id id)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();// dbContext nesnesi null m�? Null de�ilse onu kullan null ise yeni olu�tur.
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
	connection ba�lnt�s�n� koparmak yok etmek


