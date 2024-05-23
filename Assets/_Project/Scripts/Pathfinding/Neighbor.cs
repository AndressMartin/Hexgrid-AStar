[System.Serializable]
public class Neighbor
{
    public int direction;
    public Node node;

    public Neighbor(int direction, Node node)
    {
        this.direction = direction;
        this.node = node;
    }
}