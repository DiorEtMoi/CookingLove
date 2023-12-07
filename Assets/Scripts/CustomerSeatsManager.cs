using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSeatsManager : MonoBehaviour
{
    public static CustomerSeatsManager instance { get; private set; }
    [SerializeField]
    private List<Seat> seats;
    [SerializeField]
    private List<Transform> spawnPosition;
    [SerializeField]
    private Transform[] customerPerfap;
    [SerializeField]
    private List<Transform> exitWays;
    private float timeSpawnCustomer = 2;


    public void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        if (IsHasSeatsEmpty())
        {
            timeSpawnCustomer -= Time.deltaTime;
            if(timeSpawnCustomer < 0)
            {
                SpawnCustomer();
                timeSpawnCustomer = 2f;
            }
        }
    }
    public void SpawnCustomer()
    {
        int index = Random.Range(0, spawnPosition.Count);
        Instantiate(customerPerfap[Random.Range(0,customerPerfap.Length)], spawnPosition[index].position, Quaternion.identity);
    }
    public bool IsHasSeatsEmpty()
    {
        foreach(Seat seat in seats)
        {
            if (!seat.IsHasExitCustomerOnSeat())
            {
                return true;
            }
        }
        return false;
    }
    public Seat GetSeatForCustomer()
    {
        foreach (Seat seat in seats)
        {
            if (!seat.IsHasExitCustomerOnSeat())
            {
                return seat;
            }
        }
        return null;
    }
    public List<Seat> GetSeatsCurrentWorking()
    {
        List<Seat> seatsWorking = new List<Seat>();
        foreach(Seat seat in seats)
        {
            if (seat.IsHasExitCustomerOnSeat())
            {
                seatsWorking.Add(seat);
            }
        }
        return seatsWorking;
    }
    public Transform GetRandomExitWay()
    {
        return exitWays[Random.Range(0, exitWays.Count)];
    }
}
