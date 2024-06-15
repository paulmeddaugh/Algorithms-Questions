using BinaryTreeStructure;
class BinaryTree () {
    Node? root = null;

    public BinaryTree (int[] ids) : this() {
        foreach (int id in ids) {
            Insert(id);
        }
    }
    public bool Insert (int id) {
        Node newNode = new(id);
        Node? current = root, parentNode = null;
        while (current != null) {
            parentNode = current;
            if (current.Id < id) {
                current = current.LeftNode;
            } else if (current.Id > id) {
                current = current.RightNode;
            } else {
                return false; // id already exists
            }
        }
        root ??= newNode;
        if (parentNode?.Id < id) {
            parentNode.SetLeftNode(newNode);
        } else if (parentNode?.Id > id) {
            parentNode.SetRightNode(newNode);
        }
        newNode.SetParentNode(parentNode);
        return true;
    }

    public Node? FindNode (int id) {
        Node? current = root;
        while (current != null) {
            if (current.Id < id) {
                current = current.LeftNode;
            } else if (current.Id > id) {
                current = current.RightNode;
            } else {
                return current;
            }
        }

        return null;
    }

    /// <summary>
    /// Time complexity: O(h).
    /// Returns true if the node is locked and false if not.
    /// </summary>
    /// <param name="id">The ID of the node to check.</param>
    /// <returns>True if the node is locked; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown when a node with the specified ID is not found.</exception>
    public bool IsLocked (int id) {
        Node? node = FindNode(id) ?? throw new Exception("node not found with id " + id);
        return node.IsLocked;
    }

    /// <summary>
    /// Time complexity: O(2h).
    /// Marks the node as locked and all parents as unlockable.
    /// Returns true if successful; otherwise, false.
    /// </summary>
    /// <param name="id">The ID of the node to lock.</param>
    /// <returns>True if the operation is successful; otherwise, false.</returns>
    /// <exception cref="Exception">
    /// Thrown when a node with the specified ID is not found or a node within this tree is locked.
    /// </exception>
    public bool Lock (int id) {
        Node? node = FindNode(id) ?? throw new Exception("node not found with id " + id);
        if (!node.IsLockable) {
            throw new Exception("a node within this tree is locked");
        }
        node.IsLocked = true;
        Node? current = node.ParentNode;
        // Marks all parents as unlockable
        while (current != null) {
            if (current.IsLockable == false) break;
            current.IsLockable = false;
            current = current.ParentNode;
        }
        return true;
    }
    
    /// <summary>
    /// Time complexity: O(2h).
    /// Marks the node as unlocked and all parents as lockable.
    /// Returns true if successful; otherwise, false.
    /// </summary>
    /// <param name="id">The ID of the node to unlock.</param>
    /// <returns>True if the operation is successful; otherwise, false.</returns>
    /// <exception cref="Exception">
    /// Thrown when a node with the specified ID is not found or a node within this tree is locked.
    /// </exception>
    public bool Unlock (int id) {
        Node? node = FindNode(id) ?? throw new Exception("node not found with id " + id);
        if (!node.IsLockable) {
            throw new Exception("a node within this tree is locked");
        }
        node.IsLocked = false;
        Node? current = node.ParentNode;
        // Remarks all parents as lockable
        while (current != null) {
            if (current.IsLocked == false) break;
            current.IsLockable = true;
            current = current.ParentNode;
        }
        return true;
    }

    /// <summary>
    /// Prints all children of the binary tree, starting each decending node's left children first, and then its right children.
    /// </summary>
    public void PrintNodeStructure () {
        PrintNodeChildren(root);
    }

    public static void PrintNodeChildren (Node? node) {
        Console.WriteLine(node);
        if (node?.LeftNode != null) {
            PrintNodeChildren(node.LeftNode);
        }
        if (node?.RightNode != null) {
            PrintNodeChildren(node.RightNode);
        }
    }
}