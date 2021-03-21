using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    
    
    public float CurrentHealth { get { return currentHealth; } private set { } }
    [SerializeField]private float maxHealth;
    public float MaxHealth { get { return maxHealth; } private set { } }
    [SerializeField]private float currentDefans;
    private Animator anim;
    private Image healthImage;

    private float currentHealth;
    [SerializeField] private float HPRegen;
    private void Awake()
    {          
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;       
        healthImage = GameObject.Find("Health Orb").GetComponent<Image>();

    }

    private void Update()
    {
        HealthRegen();
    }

    public void TakeDamage(float amount)
    {
        amount = DamageCalculation(amount);
    }

    private void HealthRegen()
    {
        currentHealth += HPRegen * Time.deltaTime;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthImage.fillAmount = currentHealth / maxHealth;
    }


    private float DamageCalculation(float amount)
    {//defans ile yansıyan hasarın hesaplanması
        amount *= (100 / (100 + currentDefans));
        return amount;
    }

    private void HealthHandler(float resultDamage)
    {
        currentHealth -= resultDamage;
        healthImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            anim.SetBool("Death", true);
        }
        Debug.Log(resultDamage + " Hasar alındı");
    }

    //For POTİONS
    public void HealthIncrease(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthImage.fillAmount = currentHealth / maxHealth;
    }
} 



