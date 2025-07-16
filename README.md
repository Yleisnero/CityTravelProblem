# City Travel Problem: Edge Utilization

## Problem Statement
Given a city represented as a tree graph where:

- Each node represents a place with a population equal to its node number

- Each edge represents a road connecting two places

- All inhabitants need to travel to one specific destination

**The challenge**: Calculate how many people will use each road (edge) when everyone travels to a single destination.

## Algorithm Overview
The algorithm uses post-order depth-first search (DFS) to compute subtree sums in one linear pass:

- Root the tree at the destination node

-  Post-order DFS to compute subtree sums
- Extract edge loads during recursion return
- Store results in a dictionary
