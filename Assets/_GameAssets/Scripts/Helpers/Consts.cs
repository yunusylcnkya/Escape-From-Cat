// Bu sınıf, oyun içinde sık kullandığımız sabit isimleri ve etiketleri saklıyor.
// Yani oyun boyunca aynı isimleri defalarca yazmak yerine buradan çağırıyoruz.
// Bu, hem hata yapmayı azaltıyor hem de kodu daha düzenli yapıyor.
public class Consts
{
    // Oyun sahnelerinin isimleri
    public struct SceneNames
    {
        public const string GAME_SCENE = "GameScene"; // Oyun sahnesi
        public const string MENU_SCENE = "MenuScene"; // Menü sahnesi
    }

    // Oyun içindeki katman isimleri
    public struct Layers
    {
        public const string GROUND_LAYER = "Ground"; // Zemin katmanı
        public const string FLOOR_LAYER = "Floor";   // Döşeme katmanı
    }

    // Oyuncunun animasyon isimleri
    public struct PlayerAnimations
    {
        public const string IS_MOVING = "IsMoving"; // Hareket ediyor mu
        public const string IS_JUMPING = "IsJumping"; // Zıplıyor mu
        public const string IS_SLIDING = "IsSliding"; // Kayıyor mu
        public const string IS_SLIDING_ACTIVE = "IsSlidingActive"; // Kayma aktif mi
    }

    // Kedinin animasyon isimleri
    public struct catAnimations
    {
        public const string IS_IDLING = "IsIdling";   // Duruyor mu
        public const string IS_WALKING = "IsWalking"; // Yürüyor mu
        public const string IS_RUNNING = "IsRunning"; // Koşuyor mu
        public const string IS_ATTACKING = "IsAttacking"; // Saldırıyor mu
    }

    // Diğer animasyonlar (örnek)
    public struct OtherAnimations
    {
        public const string IS_SPATULA_JUMPING = "IsSpatulaJumping"; // Spatula ile zıplıyor mu
    }

    // Oyundaki farklı buğday türleri
    public struct WheatTypes
    {
        public const string GOLD_WHEAT = "GoldWheat";   // Altın buğday
        public const string HOLY_WHEAT = "HolyWheat";   // Kutsal buğday
        public const string ROTTEN_WHEAT = "RottenWheat"; // Çürük buğday
    }
}
