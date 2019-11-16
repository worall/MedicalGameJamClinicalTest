using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    List<CardContent> cards = new List<CardContent>();
    List<List<CardContent>> pools = new List<List<CardContent>>();

    private int nbAvailablePools = 25;

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate chracter

    private static DeckManager _instance;
    public static DeckManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        this.parseResource();
        this.initPools();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Pick a card in the specified turn
    public CardContent draw(int turn)
    {
        if (turn >= this.pools.Count) {
            Debug.Log("No pool available for this turn !");
            return null;
        }

        int nbCards = this.pools[turn].Count;

        if (nbCards < 1) {
          Debug.Log("No cards available for this turn !");
          return null;
        }

        int selected = Random.Range(0, nbCards);
        CardContent nextCard = this.pools[turn][selected];

        if (nextCard.popout) this.popout(nextCard, turn);

        return nextCard;
    }

    // Read data from external file (csv)
    private void parseResource()
    {
        TextAsset rawContent = Resources.Load<TextAsset>("cards");

        string[] records = rawContent.text.Split(lineSeperater);

        foreach (string record in records)
        {
            string[] row = record.Split(fieldSeperator);

            // end line?
            if (row.Length == 1) {
                continue;
            }

            CardContent card = this.mapDataToCard(row);
            cards.Add(card);
        }
    }

    // Sort all available cards in their dedicated pools
    private void initPools()
    {
        // Init empty pools
        for (int i = 0; i < nbAvailablePools; i++)
        {
            this.pools.Add(new List<CardContent>());
        }

        // Populate pools
        foreach (CardContent card in this.cards)
        {
            string[] turns = card.turns.Split(':');
            foreach (string rawTurn in turns)
            {
                int turn;
                int.TryParse(rawTurn, out turn);

                if (turn > nbAvailablePools - 1) {
                  Debug.Log("Not enough pools available");
                  continue;
                }

                this.pools[turn].Add(card);
            }
        }
    }

    private void popout(CardContent card, int fromTurn)
    {
        for (int i = fromTurn + 1; i < nbAvailablePools; i++)
        {
            int idx = this.pools[i].FindIndex((item) => item.name == card.name);
            if (idx >= 0) this.pools[i].RemoveAt(idx);
        }
    }

    private CardContent mapDataToCard(string[] data)
    {
        CardContent card = new CardContent();

        // Desc'
        card.name = data[0];
        card.situation = data[1];

        // Effect yes
        card.yes.choice = data[2];
        card.yes.debrief = data[4];
        card.yes.implication = (data[6] == "") ? 0 : int.Parse(data[6]);
        card.yes.rigueur = (data[7] == "") ? 0 : int.Parse(data[7]);
        card.yes.patients = (data[8] == "") ? 0 : int.Parse(data[8]);
        card.yes.argent = (data[9] == "") ? 0 : int.Parse(data[9]);
        card.yes.cost = (data[14] == "") ? 0 : int.Parse(data[14]);

        // Effect no
        card.no.choice = data[3];
        card.no.debrief = data[5];
        card.no.implication = (data[10] == "") ? 0 : int.Parse(data[10]);
        card.no.rigueur = (data[11] == "") ? 0 : int.Parse(data[11]);
        card.no.patients = (data[12] == "") ? 0 : int.Parse(data[12]);
        card.no.argent = (data[13] == "") ? 0 : int.Parse(data[13]);
        card.no.cost = (data[15] == "") ? 0 : int.Parse(data[15]);

        // Costs
        card.image = data[16];
        card.turns = data[17];
        card.popout = (data[18] == "1") ? true : false;

        return card;
    }
}
