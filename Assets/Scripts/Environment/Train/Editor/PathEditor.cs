﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]

public class PathEditor : Editor {

    PathCreator creator;
    Path Path
    {
        get
        {
            return creator.path;
        }
    }

    const float segmentSelectDistanceThreshold = .1f;
    int selectedSegmentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Create New"))
        {
            Undo.RecordObject(creator, "Create New");
            creator.CreatePath();
        }

        bool isClosed = GUILayout.Toggle(Path.IsClosed, "Closed");
        if(isClosed != Path.IsClosed)
        {
            Undo.RecordObject(creator, "Toggle Closed");
            Path.IsClosed = isClosed;
        }
      
        bool autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto Set Control Points");
        if (autoSetControlPoints != Path.AutoSetControlPoints)
        {
            Undo.RecordObject(creator, "Toggle Auto Set Controls");
            Path.AutoSetControlPoints = autoSetControlPoints;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            if (selectedSegmentIndex != -1)
            {
                Undo.RecordObject(creator, "Split Segment");
                Path.SplitSegment(mousePos, selectedSegmentIndex);
            }
            else if (!Path.IsClosed)
            { 
                Undo.RecordObject(creator, "Add Segment");
                Path.AddSegment(mousePos);
            }
        }

        if(guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
        {
            float minDstToAnchor = creator.anchorDiameter * .5f;
            int closestAnchorIndex = -1;

            for(int i = 0; i < Path.NumPoints; i += 3)
            {
                float dst = Vector2.Distance(mousePos, Path[i]);
                if(dst < minDstToAnchor)
                {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }

            if(closestAnchorIndex != -1)
            {
                Undo.RecordObject(creator, "Delete Segment");
                Path.DeleteSegment(closestAnchorIndex);
            }
        }

        if (guiEvent.type == EventType.MouseMove)
        {
            float minDistToSegment = segmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for (int i = 0; i < Path.NumSegments; i++)
            {
                Vector2[] points = Path.GetPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                if (dst < minDistToSegment)
                {
                    minDistToSegment = dst;
                    newSelectedSegmentIndex = i;
                }
            }

            if(newSelectedSegmentIndex != selectedSegmentIndex)
            {
                selectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }
    }

    void Draw()
    {
        for(int i = 0; i<Path.NumSegments; i++)
        {
            Vector2[] points = Path.GetPointsInSegment(i);
            if (creator.displayControlPoints)
            {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segmentCol = (i == selectedSegmentIndex && Event.current.shift) ? creator.selectedSegmentCol : creator.segmentCol;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentCol, null, 2);
        }



        for (int i = 0; i < Path.NumPoints; i++)
        {
            if (i % 3 == 0 || creator.displayControlPoints)
            {
                Handles.color = (i % 3 == 0) ? creator.anchorCol : creator.controlCol;
                float handleSize = (i % 3 == 0) ? creator.anchorDiameter : creator.controlDiameter;
                Vector2 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
                if (Path[i] != newPos)
                {
                    Undo.RecordObject(creator, "Move Point");
                    Path.MovePoint(i, newPos);
                }
            }
        }
    }

    void OnEnable()
    {
        creator = (PathCreator)target;
        if(creator.path == null)
        {
            creator.CreatePath();
        }
    }
}
