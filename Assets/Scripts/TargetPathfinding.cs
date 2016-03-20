using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPathfinding : MonoBehaviour
{
    int genauigkeit = 1; //Genauigkeit in Pixel bzw. Abstände der Knoten
    List<Vector2> path = new List<Vector2>(); //Den endgültigen Pfad
    PathPoint startpathpoint; //StartPathPoint
    Vector2 target; //Position des Ziels
    List<PathPoint> closedlist = new List<PathPoint>(); //Knoten die shcon abgeschlossen sind
    List<PathPoint> openlist = new List<PathPoint>(); //Knoten die noch überprüft werden müssen
    //TODO: SOrtedList
    float size;

    public List<Vector2> Pathfinding(Vector2 startposition, Vector2 target, float size) //Gibt eine List mit Wegpunkten zurück
    {
        //this.size = size;
        path.Clear();
        openlist.Clear();
        closedlist.Clear();
        this.size = size;
        this.target = target; //target wird gesetzt
        startpathpoint = new PathPoint(startposition, null, 0); //Startpathpoint mit Aufwand 0
        PathPoint currentNode; //Knoten der gerade überprüft werden soll
        openlist.Add(startpathpoint); //Startpunkt wird der openList hinzugefügt
        int sicherung = 0;
        do
        {
            sicherung++;
            if (sicherung > 100)
            {
                Debug.Log(">100");
                break;
            }
            /*
            foreach (PathPoint t in openlist)
            {
                if (t.GetAufwand() != 0)
                    Debug.DrawLine(t.GetPosition(), t.GetPosition() + new Vector2(0.1f, 0.1f), Color.red, 5);
            }
            */
            currentNode = FindMin(openlist); //Der Wegpunkt mit dem niedrigsten Aufwand wird aus der openlist herausgenommen
            if (CheckReachable(target, currentNode.GetPosition(), size)) //Wenn der Knoten das Ziel erreicht hat dann wurde der Pfad gefunden
            {
                PathFound(currentNode); //Pfad wird erstellt
                return path;
            }

            closedlist.Add(currentNode); //Der Knoten wurde "abgearbeitet" und der Closelist hinzugefügt
            NachfolgerKnoten(currentNode); //NachfolgerKnoten werden überprfüt und hinzugefügt

        }
        while (openlist.Count != 0); //Solange in der Openlist Knoten enthalten sind
        Debug.Log("Keinen Pfad gefunden");
        return path;
    }

    void NachfolgerKnoten(PathPoint currentNode) //Fügt alle noch neuen NachfolgerKnoten der OpenList hinzu
    {
        Vector2 directionx = new Vector2(genauigkeit, 0);
        Vector2 directiony = new Vector2(0, genauigkeit);
        Vector2 position = currentNode.GetPosition();
        Vector2 nextposition;
        List<PathPoint> checkpoints = new List<PathPoint>();


        nextposition = position + directionx;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));
        /*
        nextposition = position + directionx + directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));

        
        nextposition = position - directionx + directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));

                nextposition = position - directionx - directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));

        
        nextposition = position + directionx - directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));
            */

        nextposition = position + directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));


        nextposition = position - directionx;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));



        nextposition = position - directiony;
        if (!CheckCollision(nextposition, size))
            checkpoints.Add(new PathPoint(nextposition, currentNode, G(currentNode) + H(nextposition)));
        //Debug.Log(G(currentNode) + "<-->" + H(nextposition));

        foreach (PathPoint t in checkpoints)
        {
            if (CheckContains(t, closedlist))//Wenn der Knoten in der ClosedList dann mit dem Nächsten Knoten weitermachen.
                continue;
            CheckAndAdd(t);

        }

    }

    void CheckAndAdd(PathPoint node)//Wenn der Knoten in der OpenList und Aufwand geringer dann hinzufügen
    {
        bool test = false;
        int index = 0;
        string IDtoFind = node.ID;
        PathPoint result = openlist.Find(item => item.ID.Equals(IDtoFind));
        for (int i = 0; i < openlist.Count; ++i)
        {
            //Debug.Log(t.ID.Equals(IDtoFind));
            if (openlist[i].ID.Equals(IDtoFind))
            {
                result = openlist[i];
                test = true;
                index = i;
                break;
            }
        }


        if (test)
        {
            //Debug.Log(node.GetAufwand() + "<-->" + result.GetAufwand());
            if (node.GetAufwand() < result.GetAufwand())
            {
                openlist.RemoveAt(index);
                openlist.Add(node);
            }
        }
        else
            openlist.Add(node);
    }

    bool CheckContains(PathPoint node, List<PathPoint> list) //Ob der Knoten in der Liste enthalten ist
    {
        if (list.Contains(node))
            return true;
        else
            return false;
    }

    int H(Vector2 nodeposition)
    {
        int dirx = Mathf.RoundToInt(nodeposition.x - target.x);
        int diry = Mathf.RoundToInt(nodeposition.y - target.y);
        return genauigkeit * (dirx + diry);
    }

    int G(PathPoint node) //Aufwand vom Startpunkt bis zum Knoten(node);)
    {
        PathPoint currentnode = node;
        PathPoint nodebefore = node.GetLastPoint();
        int aufwand = 1;
        while (currentnode.GetAufwand() != 0)
        {
            aufwand += genauigkeit;
            currentnode = nodebefore;
            nodebefore = currentnode.GetLastPoint();
        }
        return aufwand;

    }

    void PathFound(PathPoint targetpoint)//Kreiert den fertigen Pfad als Vector2 Wegpunkte
    {
        path.Add(targetpoint.GetPosition());
        if (targetpoint.GetAufwand() != 0)
            PathFound(targetpoint.GetLastPoint());
    }

    PathPoint FindMin(List<PathPoint> list)//Findet den nächsten Knoten mit dem kleinsten Aufwand und entfernt ihn aus der OpenList
    {
        //VIelleicht doch nicht sortieren
        int help = 0;
        float save = Mathf.Infinity;
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].GetAufwand() < save)
            {
                help = i;
                save = list[i].GetAufwand();
            }
        }
        PathPoint savepoint = list[help];
        //Debug.Log("Wert" + save + " Position: " + savepoint.GetPosition());
        list.RemoveAt(help);
        return savepoint;
    }

    bool CheckCollision(Vector2 position, float size)//Überprüft ob das Objekt mit einer gewissen Größe (size) eine Wand trifft
    {
        Vector2 directionx = new Vector2(size, 0);
        Vector2 directiony = new Vector2(0, size);
        Vector2 vectorsize = directionx + directiony;
        LayerMask mask = -1;
        //RaycastHit2D hit = Physics2D.Raycast(position, directionx, size);
        //Debug.Log("Check-Collision: " + position);
        Collider2D[] hit = Physics2D.OverlapAreaAll(position - vectorsize, position + vectorsize);
        foreach (Collider2D c in hit)
        {
            if (c.CompareTag("Wall"))
            {
                //Debug.Log("True");
                return true;
            }
        }
        return false;
        /*
        RaycastHit2D hit = Physics2D.Raycast(position, directionx, size);
                if (hit.collider != null)
            if (hit.collider.CompareTag("Wall"))
                return true;

        hit = Physics2D.Raycast(position, -directiony, size);
        if (hit.collider != null)
            if (hit.collider.CompareTag("Wall"))
                return true;

        hit = Physics2D.Raycast(position, -directionx, size);
        if (hit.collider != null)
            if (hit.collider.CompareTag("Wall"))
                return true;

        hit = Physics2D.Raycast(position, directiony, size);
        if (hit.collider != null)
            if (hit.collider.CompareTag("Wall"))
                return true;
                */


    }

    bool CheckReachable(Vector2 target, Vector2 position, float size)//Überprüft ob die Position das Target trifft mit einer gewissen Größe
    {
        if (target.x + size >= position.x && target.x - size <= position.x && target.y - size <= position.y && target.y + size >= position.y)
            return true;
        else
            return false;
    }
}
