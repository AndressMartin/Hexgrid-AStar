using System;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Tile))]
public class Node : MonoBehaviour, ICell
{
    private static Dictionary<Hex, Node> nodes = new();

    [Range(0, 5)]
    [SerializeField] private int direction;
    
    [SerializeField] private Tile tile;
    
    [SerializeField] private List<Neighbor> neighbors = new();

    public Hex Hex => transform.position.ToHex();
    public Hex LocalHex => transform.localPosition.ToHex();
    public int Weight => Tile.Data.Weight;
    public Tile Tile => tile;
    public Vector3 CellPosition => transform.position;

    private void Start()
    {
        Tile.Initialize(this);
    }

    private void ApplyTransform()
    {
        Vector3 newPos = LocalHex.ToWorld();
        transform.localPosition = newPos;
        transform.localRotation = Quaternion.Euler(0, -60f * direction, 0);
    }

    private void OnEnable()
    {
        RegisterNode();
        UpdateNeighbors();
    }

    private void OnDisable()
    {
        UnregisterNode();
        UpdateNeighbors();
    }

    private void RegisterNode()
    {
        Hex hex = this.Hex;
        if (!nodes.ContainsKey(hex))
        {
            nodes.Add(hex, this);
        }
        else
        {
            Debug.LogWarning($"A node at {hex} is already registered.");
        }
    }

    private void UnregisterNode()
    {
        Hex hex = this.Hex;
        if (nodes.ContainsKey(hex))
        {
            nodes.Remove(hex);
        }
    }

    private void UpdateNeighbors()
    {
        neighbors.Clear();

        for (int i = 0; i < Hex.AXIAL_DIRECTIONS.Length; i++)
        {
            Hex neighborHex = Hex + Hex.AXIAL_DIRECTIONS[i];
            if (nodes.TryGetValue(neighborHex, out Node neighborNode) && neighborNode.Weight >= 0) //Valid weights are 1 or greater
            {
                neighbors.Add(new Neighbor(i, neighborNode));
                neighborNode.RegisterNeighbor((i + 3) % 6, this); // Add this node as the neighbor's neighbor.
            }
        }

        // Remove non-existent neighbors from other nodes' lists
        for (int i = 0; i < neighbors.Count; i++)
        {
            Neighbor neighbor = neighbors[i];
            if (neighbor.node == null || !nodes.ContainsValue(neighbor.node))
            {
                neighbors.RemoveAt(i);
                i--;
            }
        }
    }

    private void RegisterNeighbor(int direction, Node neighborNode)
    {
        if (!neighbors.Exists(n => n.direction == direction && n.node == neighborNode))
        {
            neighbors.Add(new Neighbor(direction, neighborNode));
        }
    }

#if UNITY_EDITOR
    protected virtual void Update()
    {
        if (!Application.isPlaying)
        {
            ApplyTransform();
            UpdateNeighbors();
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.Label(transform.position, Hex.ToString());
    }
#endif
}