using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    List<CardContent> cards = new List<CardContent>();
    List<List<CardContent>> pools = new List<List<CardContent>>();

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate chracter

    // Start is called before the first frame update
    void Start()
    {
      this.parseResource();
      this.initPools();

      // DEBUG
      CardContent next = this.draw(1);
      Debug.Log(next.name);
    }

    CardContent draw(int turn) {
      int nbCards = this.pools[turn].Count;
      int selected = Random.Range(0, nbCards);
      CardContent nextCard = this.pools[turn][selected];

      if (nextCard.popout) this.popout(nextCard, turn);

      return nextCard;
    }

    private void parseResource() {
      TextAsset rawContent = Resources.Load<TextAsset>("cards");

      string[] records = rawContent.text.Split(lineSeperater);

      foreach (string record in records) {
        string[] row = record.Split(fieldSeperator);

        CardContent card = new CardContent();
        card.name = row[0];
        card.situation = row[1];
        card.choiceLeft = row[2];
        card.choiceRight = row[3];
        card.resLeft = row[4];
        card.resRight = row[5];
        card.time = row[14];
        card.turns = row[15];

        cards.Add(card);
      }
    }

    private void initPools() {
      // Init empty pools
      for (int i = 0; i < 20; i++) {
        this.pools.Add(new List<CardContent>());
      }

      // Populate pools
      foreach (CardContent card in this.cards) {
        string[] turns = card.turns.Split(':');
        foreach (string rawTurn in turns) {
          int turn;
          int.TryParse(rawTurn, out turn);
          this.pools[turn].Add(card);
        }
      }
    }

    private void popout(CardContent card, int fromTurn) {
      for (int i = fromTurn + 1; i < pools.Count; i++) {
        int idx = this.pools[i].FindIndex((item) => item.name == card.name);
        if (idx >= 0) this.pools[i].RemoveAt(idx);
      }
    }

    private void updatePools() {
      // Modify pools if need be
    }
}
