using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMenuManager : MonoBehaviour
{
    public static OrderMenuManager instance { get; private set; }
    [SerializeField]
    private List<OrderFoodSO> foodSO;

    private void Awake()
    {
        instance = this;
    }

    public OrderFoodSO GetRandomOrderFoodSO()
    {
        return foodSO[UnityEngine.Random.Range(0, foodSO.Count)];
    }
    public bool DeliveryFoodForCustomer(List<FoodObjectSO> foodSO,Transform platePostion)
    {
        foreach(Seat seat in CustomerSeatsManager.instance.GetSeatsCurrentWorking())
        {
            bool isExist = false;
            CustomerService customer = seat.GetCustomerOnSeat();
            OrderFoodSO order = null;
            foreach(OrderFoodSO orderFood in customer.GetOrderFood())
            {
                if(orderFood.foodCompleted.Count != foodSO.Count)
                {
                    continue;
                }
                order = orderFood;
                foreach (FoodObjectSO foodCustomer in orderFood.foodCompleted)
                {
                    bool isMatching = false;
                    foreach (FoodObjectSO foodDelivery in foodSO)
                    {
                        if(foodCustomer == foodDelivery)
                        {
                            isMatching = true;
                            isExist = true;
                            break;
                        }
                    }
                    if(isMatching == false)
                    {
                        isExist = false;
                        break;
                    }
                }
                if (isExist)
                {
                    Debug.Log("Delivery Food For Customer");
                    Transform orderVisual = Instantiate(order.perfap, platePostion.position, Quaternion.identity);
                    Transform orderPosition = seat.GetPostionOfOrder(order);
                    orderVisual.DOMove(orderPosition.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(orderVisual.gameObject);
                    });
                    customer.RemoveOrderFood(order);
                    return true;
                }
            }
        }
        return false;
    }
    public bool DeliveryFoodForCustomer(FoodObjectSO foodSO, Transform platePostion)
    {
        foreach (Seat seat in CustomerSeatsManager.instance.GetSeatsCurrentWorking())
        {
            bool isExist = false;
            CustomerService customer = seat.GetCustomerOnSeat();
            OrderFoodSO order = null;
            foreach (OrderFoodSO orderFood in customer.GetOrderFood())
            {
                if (orderFood.foodCompleted.Count > 2)
                {
                    continue;
                }
                order = orderFood;
                foreach (FoodObjectSO foodCustomer in orderFood.foodCompleted)
                {
                    if(foodCustomer == foodSO)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    Transform orderVisual = Instantiate(order.perfap, platePostion.position, Quaternion.identity);
                    Transform orderPosition = seat.GetPostionOfOrder(order);
                    orderVisual.DOMove(orderPosition.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(orderVisual.gameObject);
                    });
                    customer.RemoveOrderFood(order);
                    return true;
                }
            }
        }
        return false;
    }
}
