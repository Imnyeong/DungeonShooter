using UnityEngine;
using WebSocketSharp;

public class WebSocketManager : MonoBehaviour
{
    public static WebSocketManager instance;

    WebSocket ws;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Connect();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Space");
        }
    }
    public void Connect()
    {
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("�ּ� :  " + ((WebSocket)sender).Url + ", ������ : " + e.Data);
        };
    }
}