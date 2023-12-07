using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerService : MonoBehaviour, IHasProgress
{
    [SerializeField]
    private List<OrderFoodSO> customerOrderMenu;
    private int moveSpeed;
    private Seat customerSeat;
    private float waitingTime;
    private float maxTime;
    private Transform exitWay;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hiAudio;
    public enum CustomerState
    {
        START,
        FINDING_SEAT,
        ORDER_FOOD,
        DONE
    }
    public CustomerState state;
    public event EventHandler OnCustomerOnSeat;
    public event EventHandler<OnCustomerOrderFoodEvetArg> OnCustomerOrderFood;
    public event EventHandler<IHasProgress.OnProgressChangedArg> OnProgressChanged;
    public event EventHandler OnCustomerServiceDone;
    public class OnCustomerOrderFoodEvetArg: EventArgs
    {
        public List<OrderFoodSO> customerOrders;
    }
    public void Awake()
    {
        moveSpeed = 4;
        waitingTime = UnityEngine.Random.Range(10, 20);
        maxTime = waitingTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(hiAudio, 0.8f);
    }
    public void Update()
    {
        switch (state) 
        {
            case CustomerState.START:
                FindingSeat();
                state = CustomerState.FINDING_SEAT;
                break;
            case CustomerState.FINDING_SEAT:
                transform.position = Vector2.MoveTowards(transform.position, customerSeat.transform.position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position,customerSeat.transform.position) < .1f)
                {
                    OrderFood();
                    OnCustomerOnSeat?.Invoke(this, EventArgs.Empty);
                    OnCustomerOrderFood.Invoke(this, new OnCustomerOrderFoodEvetArg
                    {
                        customerOrders = this.customerOrderMenu
                    }); 
                    state = CustomerState.ORDER_FOOD;
                }
                break;
            case CustomerState.ORDER_FOOD:
                waitingTime -= Time.deltaTime;
                if(waitingTime < 0)
                {
                    state = CustomerState.DONE;
                    waitingTime = 0;
                    exitWay = CustomerSeatsManager.instance.GetRandomExitWay();
                    OnCustomerServiceDone?.Invoke(this, EventArgs.Empty);
                    customerSeat.SetSeatForCustomer(null);
                    SetCustomerSeat(null);
                    GameManager.instance.UpdateNotDoneOrder();
                }
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArg
                {
                    progress = waitingTime / maxTime
                }); 
                break;
            case CustomerState.DONE:
                HandleWhenOrderDone();
                break;
        }
    }
    public void FindingSeat()
    {
        Seat mySeat = CustomerSeatsManager.instance.GetSeatForCustomer();
        SetCustomerSeat(mySeat);
        mySeat.SetSeatForCustomer(this);
    }
    public void HandleWhenOrderDone()
    {
        transform.position = Vector2.MoveTowards(transform.position, exitWay.position, moveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, exitWay.position) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
    public void OrderFood()
    {
        int randomOrder = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < randomOrder; i++)
        {
            customerOrderMenu.Add(OrderMenuManager.instance.GetRandomOrderFoodSO());
        }
    }
    public void SetCustomerSeat(Seat seat)
    {
        this.customerSeat = seat;
    }
    public List<OrderFoodSO> GetOrderFood()
    {
        return customerOrderMenu;
    }
    public void RemoveOrderFood(OrderFoodSO orderFood)
    {
        customerOrderMenu.Remove(orderFood);
        OnCustomerOrderFood?.Invoke(this, new OnCustomerOrderFoodEvetArg
        {
            customerOrders = customerOrderMenu
        });
        if (customerOrderMenu.Count.Equals(0))
        {
            state = CustomerState.DONE;
            exitWay = CustomerSeatsManager.instance.GetRandomExitWay();
            OnCustomerServiceDone?.Invoke(this, EventArgs.Empty);
            customerSeat.SetSeatForCustomer(null);
            SetCustomerSeat(null);
            GameManager.instance.AddPoint(50);
        }
        audioSource.Play();
    }

}
