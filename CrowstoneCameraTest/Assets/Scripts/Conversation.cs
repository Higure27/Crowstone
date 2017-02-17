using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Conversation {

    /**
 * A dictionary which connects a string label to a key-value pair with a DialogueNode as 
 * the key, and a LinkedList of its connected DialogueNodes as the value.
 **/
    Dictionary<string, DialogueNode> labelMap;
    Dictionary<string, LinkedList<string>> adjacenceyList;

    public Conversation()
    {
        labelMap = new Dictionary<string, DialogueNode>();
        adjacenceyList = new Dictionary<string, LinkedList<string>>();
    }

    /*
     * Takes a filename as input and parses the file
     **/
    public void ParseFile(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string[] line;
                while ((line = sr.ReadLine().Split(new char[] { '~' })) != null)
                {
                    if (line[0].Equals("s"))
                    {
                        AddStandaloneDialogue(line[1], line[2], line[3], Convert.ToBoolean(line[4]));
                    }
                    else if (line[0].Equals("t"))
                    {
                        AddDialogueTo(line[1][0] + "", line[1][2] + "", line[2], line[3], Convert.ToBoolean(line[4]));
                    }
                    else if (line[0].Equals("c"))
                    {
                        ConnectDialogue(line[1][0] + "", line[1][2] + "");
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
    }

    public void ParseXML(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                List<string> line;
                while ((line = StringSplit(sr.ReadLine(), ' ', '"')) != null)
                {
                    if (line[0].Equals("<node"))
                    {

                    }
                }
            }
        }
        catch (Exception e) { }
    }

    private List<string> StringSplit(string s, char delim, char pauser)
    {
        if (delim == pauser || s == "" || s == null)
        {
            return null;
        }
        string current = "";
        bool paused = false;
        List<string> result = new List<string>();
        foreach (char c in s)
        {
            if (c == pauser)
            {
                paused = !paused;
            }
            else if (c == delim)
            {
                if (!paused)
                {
                    result.Add(current);
                    current = "";
                }
                else
                {
                    current += c;
                }
            }
            else
            {
                current += c;
            }
        }
        if (!paused)
            result.Add(current);
        return result;
    }

    /*
     * Adds a new DialogueNode to this conversation with no connections if another with the
     * same label does not already exist. Returns true if successful, false otherwise.
     **/
    public bool AddStandaloneDialogue(string l, string a, string b, bool exit)
    {
        DialogueNode n;
        if (!labelMap.TryGetValue(l, out n))
        {
            n = new DialogueNode(l, a, b, exit);
            labelMap.Add(l, n);
            adjacenceyList.Add(l, new LinkedList<string>());
            return true;
        }
        return false;
    }

    /*
     * Adds a new DialogueNode to this coversation with label pl connected from l if such a connection
     * does not already exist. This connection is one way from l to pl. Returns true if successful, false otherwise.
     **/
    public bool AddDialogueTo(string l, string pl, string a, string b, bool exit)
    {
        DialogueNode L;
        DialogueNode PL;
        if (labelMap.TryGetValue(l, out L) && !labelMap.TryGetValue(pl, out PL))
        {
            PL = new DialogueNode(l, a, b, exit);
            labelMap.Add(pl, PL);
            adjacenceyList.Add(pl, new LinkedList<string>());
            adjacenceyList[l].AddLast(pl);
            return true;
        }
        return false;
    }

    /*
     * Connects two existing DialogueNodes with labels l and pl if two such DialogueNodes exist. This
     * connection is one way from l to pl. Returns true if successful, false otherwise.
     **/
    public bool ConnectDialogue(string l, string pl)
    {
        DialogueNode L;
        DialogueNode PL;
        if (labelMap.TryGetValue(l, out L) && labelMap.TryGetValue(pl, out PL))
        {
            adjacenceyList[l].AddLast(pl);
            return true;
        }
        return false;
    }

    public void TraverseConversation(string startingNode)
    {
        DialogueNode start;
        if (labelMap.TryGetValue(startingNode, out start))
        {
            Console.WriteLine(start.dialogueB);
            foreach (string option in adjacenceyList[startingNode])
            {
                Console.WriteLine("\t" + option + ": " + labelMap[option].dialogueA);
            }
            TraverseConversation(startingNode, Console.ReadLine());
        }
    }

    private void TraverseConversation(string from, string to)
    {
        if (adjacenceyList[from].Contains(to))
        {
            Console.WriteLine(labelMap[to].dialogueB);
            foreach (string option in adjacenceyList[to])
            {
                Console.WriteLine("\t" + option + ": " + labelMap[option].dialogueA);
            }
            TraverseConversation(to, Console.ReadLine());
        }
    }

    public string[] ListDialogueOptions(string from)
    {
        string[] result = null;
        LinkedList<string> connections;
        if (adjacenceyList.TryGetValue(from, out connections))
        {
            result = new string[connections.Count];
            int i = 0;
            foreach(string dialogueLabel in connections)
            {
                result[i] = labelMap[dialogueLabel].dialogueA;
                i++;
            }
        }
        return result;
    }

    public Dictionary<string, string> ListDialogueConnections(string from)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        LinkedList<string> connections;
        if (adjacenceyList.TryGetValue(from, out connections))
        {
            foreach (string dialogueLabel in connections)
                result.Add(dialogueLabel, labelMap[dialogueLabel].dialogueA);
        }
        return result;
    }

    public string GetDialogueB(string node)
    {
        return labelMap[node].dialogueB;
    }

    public bool GetExitValue(string node)
    {
        return labelMap[node].exit;
    }
}

public class DialogueNode
{
    string label;
    public readonly string dialogueA = "";
    public readonly string dialogueB = "";
    public bool show { get; set; }
    public bool exit;

    /*
     * Constructs a DialogueNode with l as the label, a as the first dialogue,
     * and b and the second dialogue. Show is true by default;
     **/
    public DialogueNode(string l, string a, string b, bool e)
    {
        label = l;
        dialogueA = a;
        dialogueB = b;
        show = true;
        exit = e;
    }
}
