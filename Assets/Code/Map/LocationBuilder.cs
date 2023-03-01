using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class LocationBuilder : MonoBehaviour
{
    [SerializeField]List<LocationSO> locations;

    private int  actualLocationIndex;
   public LocationSO GenerateLocation()
   {
        actualLocationIndex = Random.Range(0, locations.Count);
        GameObject currentLocation =  Instantiate(locations[actualLocationIndex].locationPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
        DisableCollidersFromAILayer(currentLocation);
        InstantiateEventGameObjects();
        return locations[actualLocationIndex];


   }

    private void DisableCollidersFromAILayer(GameObject currentLocation)
    {
        InstantiatedEvent instantiated= currentLocation.GetComponentInChildren<InstantiatedEvent>();

        instantiated.Initialise(currentLocation.GetComponentInChildren<Grid>().gameObject);
    }

    public void InstantiateEventGameObjects()
    {
        //int i = 0;

        for (int i = 0; i < locations[actualLocationIndex].areasOfEvents.Count; i++)
        {
            //print(i);
            if (i%2==0)
            {
                
                Vector2Int currentAreaUpperLeftPoint= locations[actualLocationIndex].areasOfEvents[i];
                Vector2Int currentAreaLowerRightPoint = locations[actualLocationIndex].areasOfEvents[i+1];

                //найти размеры текущей локации 
                Vector2Int currentAreaProportion = GetCurrentAreaProportion(currentAreaUpperLeftPoint, currentAreaLowerRightPoint);

                //пробежаться по масиву евентов и посмотреть соответсвует ли оно пропорциям евента 
                List<EventSO> events = GetListOfSuitableEvents(locations[actualLocationIndex].events, currentAreaProportion);  
               
                //выбираем ивет из подошедшего
                EventSO currentEvent = events[Random.Range(0, events.Count)];

                //рандомно решить будет этот тайл с ивентом или без (event or eventfree)
                int isEventZone = Random.Range(0,2);

                //удалить его из выборки  


                //иниализировать подошедший ивент 
                InitialiseCurrentEvent(currentEvent, isEventZone, currentAreaUpperLeftPoint, currentAreaLowerRightPoint);


            }

        }
        
    }


    private List<EventSO> GetListOfSuitableEvents(List<EventSO> events, Vector2Int currentAreaProportion)
    {
        List<EventSO> tmpEvents = new List<EventSO>();
        foreach (EventSO eventSO in events)
        {
            if (eventSO.proportions == currentAreaProportion)
            {
                tmpEvents.Add(eventSO);
               // print(eventSO.proportions);
            }
        }
        return tmpEvents;
    }

    private Vector2Int GetCurrentAreaProportion(Vector2Int currentAreaUpperLeftPoint, Vector2Int currentAreaLowerRightPoint)
    {
        Vector2Int CurrentAreaProportion=
            new Vector2Int(Mathf.Abs(currentAreaUpperLeftPoint.x - currentAreaLowerRightPoint.x) + 1
                    ,Mathf.Abs(currentAreaUpperLeftPoint.y - currentAreaLowerRightPoint.y) + 1);
       // print(CurrentAreaProportion);
        return CurrentAreaProportion;
    }

    private void InitialiseCurrentEvent(EventSO currentEvent,int isEventZone, Vector2Int currentAreaUpperLeftPoint, Vector2Int currentAreaLowerRightPoint)
    {
        Vector3 eventPosition = new Vector3(0, 0, 0);
        GameObject gameObject;

        if (isEventZone == 1)   
            gameObject = Instantiate(currentEvent.eventPrefab, eventPosition, Quaternion.identity, transform);
        else 
            gameObject = Instantiate(currentEvent.eventFreePrefab, eventPosition, Quaternion.identity, transform);
       
        InstantiatedEvent instantiatedEvent = gameObject.GetComponentInChildren<InstantiatedEvent>();
        instantiatedEvent.Initialise(gameObject);

        //перекопировать тайлы в нужном порядке и установить геймобжекты в нужном месте 
        SetTilesOnRightPlace(gameObject.GetComponentInChildren<Grid>(), currentAreaUpperLeftPoint, currentAreaLowerRightPoint, currentEvent, isEventZone, instantiatedEvent);
       
    }

    private void SetTilesOnRightPlace(Grid grid, Vector2Int startPos, Vector2Int endPos, EventSO eventSO, int isEventZone, InstantiatedEvent instantiatedEvent)
    {
        Tilemap[] tilemaps = grid.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            
            for (int offsetX=0, xPos = startPos.x; xPos >= endPos.x; offsetX--, xPos--)
            {
                for (int offsetY=0, yPos = startPos.y; yPos >= endPos.y; offsetY--, yPos--)
                {
                    if (isEventZone==1)
                    {
                        //поштучно перекопируем тайлы
                        tilemap.SetTile(new Vector3Int(xPos, yPos, 0), tilemap.GetTile(new Vector3Int(eventSO.startCopyEventPosition.x + offsetX, eventSO.startCopyEventPosition.y + offsetY, 0)));
                        
                        //удаляем старые 
                        tilemap.SetTile(new Vector3Int(eventSO.startCopyEventPosition.x + offsetX, eventSO.startCopyEventPosition.y + offsetY, 0), null);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(xPos, yPos, 0), tilemap.GetTile(new Vector3Int(eventSO.startCopyEventFreePosition.x + offsetX, eventSO.startCopyEventFreePosition.y + offsetY, 0)));

                        tilemap.SetTile(new Vector3Int(eventSO.startCopyEventFreePosition.x + offsetX, eventSO.startCopyEventFreePosition.y + offsetY, 0), null );
                    }
                    if (xPos== (startPos.x+endPos.x)/2 && yPos== (startPos.y + endPos.y) / 2 )
                    {

                        int xMyltiplyer = 2;
                        int yMyltiplyer = 2;

                        if (eventSO.proportions.x> eventSO.proportions.y )
                            xMyltiplyer = 3;
                        if (eventSO.proportions.x < eventSO.proportions.y)
                            yMyltiplyer = 3;

                        int eventOffsetX = eventSO.proportions.x / xMyltiplyer;
                        int eventOffsetY= eventSO.proportions.y/ yMyltiplyer;
                        instantiatedEvent.gameObjectLocationTransform.position= tilemap.CellToLocal(new Vector3Int(xPos+ eventOffsetX, yPos+eventOffsetY,0));
                    }
                }
                 
                
            }
        }
    }


}
