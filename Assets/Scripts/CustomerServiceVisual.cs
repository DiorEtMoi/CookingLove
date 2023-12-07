using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerServiceVisual : MonoBehaviour
{
    [SerializeField]
    private Transform orderFrame;
    [SerializeField]
    private List<Transform> spawnFoodPostion;
    [SerializeField]
    private Transform waitingBarSprite;
    [SerializeField]
    private Transform tickTransform;
    [SerializeField]
    private Animator animator;
    private CustomerService customer;

   
    private void Customer_OnCustomerOnSeat(object sender, System.EventArgs e)
    {
        orderFrame.gameObject.SetActive(true);
    }
    
    public void SetCustomer(CustomerService customer)
    {
        if(customer == null)
        {
            ClearEvent();
        }
        else
        {
            this.customer = customer;
            this.customer.OnCustomerOnSeat += Customer_OnCustomerOnSeat;
            this.customer.OnCustomerOrderFood += Customer_OnCustomerOrderFood;
            this.customer.OnProgressChanged += Customer_OnProgressChanged;
            this.customer.OnCustomerServiceDone += Customer_OnCustomerServiceDone;
        }
        
    }
    public void ClearEvent()
    {
        this.customer.OnCustomerOnSeat -= Customer_OnCustomerOnSeat;
        this.customer.OnCustomerOrderFood -= Customer_OnCustomerOrderFood;
        this.customer.OnProgressChanged -= Customer_OnProgressChanged;
        this.customer.OnCustomerServiceDone -= Customer_OnCustomerServiceDone;
    }

    private void Customer_OnCustomerServiceDone(object sender, System.EventArgs e)
    {
        ClearChildOnSpawnPostion();
        tickTransform.gameObject.SetActive(true);
        StartCoroutine(CloseOrderFrameCor());
    }
    private IEnumerator CloseOrderFrameCor()
    {
        animator.SetTrigger("Done");
        yield return new WaitForSeconds(0.5f);
        tickTransform.gameObject.SetActive(false);
        orderFrame.gameObject.SetActive(false);

    }
    private void Customer_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArg e)
    {
        waitingBarSprite.gameObject.transform.localScale = new Vector2(1, e.progress);
    }

    private void Customer_OnCustomerOrderFood(object sender, CustomerService.OnCustomerOrderFoodEvetArg e)
    {
        ClearChildOnSpawnPostion();
        if(e.customerOrders.Count == 1)
        {
            Instantiate(e.customerOrders[0].perfap, spawnFoodPostion[2]);
        }
        else
        {
            for (int i = 0; i < e.customerOrders.Count; i++)
            {
                if (e.customerOrders.Count % 2 == 0)
                {
                    Instantiate(e.customerOrders[i].perfap, spawnFoodPostion[i * 2 + 1]);
                }
                else
                {
                    Instantiate(e.customerOrders[i].perfap, spawnFoodPostion[i * 2]);
                }
            }
        }
    }
    public void ClearChildOnSpawnPostion()
    {
        foreach(Transform spawnPos in spawnFoodPostion)
        {
            foreach(Transform child in  spawnPos) 
            {
                Destroy(child.gameObject);
            }
        }
    }
    public Transform GetPostionOfOrderFood(OrderFoodSO order)
    {
        foreach(Transform spawn in spawnFoodPostion)
        {
            if(spawn.childCount > 0)
            {
                string checkName = order.perfap.name;
                string nameOfSpawn = spawn.GetChild(0).name.Split("(Clone)")[0];
                if (nameOfSpawn.Equals(checkName))
                {
                    Debug.Log(checkName + " " + nameOfSpawn);
                    return spawn;
                }
                
            }
        }

        return null;
    }
}
