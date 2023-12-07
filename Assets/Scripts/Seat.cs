using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField]
    private CustomerService customer;
    private CustomerServiceVisual visual;

    private void Awake()
    {
        visual  = GetComponent<CustomerServiceVisual>();
    }
    public void SetSeatForCustomer(CustomerService customer)
    {
        this.customer = customer;
        visual.SetCustomer(customer);
        
    }
    public CustomerService GetCustomerOnSeat()
    {
        return this.customer;
    }
    public bool IsHasExitCustomerOnSeat()
    {
        return this.customer != null;
    }
    public Transform GetPostionOfOrder(OrderFoodSO order)
    {
       return visual.GetPostionOfOrderFood(order);
    }
}
