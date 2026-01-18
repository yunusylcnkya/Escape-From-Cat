using UnityEngine;

// IDamageable bir "sözleşme" gibidir. 
// Yani, bu sözleşmeyi kullanan her obje, "GiveDamage" adlı bir şey yapacağını söylüyor.
// Bir başka deyişle, bu obje hasar verebilmeli demek.
// Rigidbody ve Transform ise oyuncuyu itmek veya yönünü bilmek için gereken bilgiler.
public interface IDamageable
{
    void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform);
}
