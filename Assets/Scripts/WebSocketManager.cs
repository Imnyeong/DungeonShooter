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
    public void Connect()
    {
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            //Debug.Log("�ּ� :  " + ((WebSocket)sender).Url + ", ������ : " + e.Data);
            //ToDo: GameManager���� ó���ϵ���
        };
    }
}