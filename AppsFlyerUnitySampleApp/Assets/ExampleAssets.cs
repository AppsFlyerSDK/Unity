//ExampleAssets.cs 
//Alexander Young 
//February 5, 2015
//Description - Creates the IAP assets so that their can be bought and used

using UnityEngine;
using System.Collections;

namespace Soomla.Store.Example															//Allows for access to Soomla API
{
	public class ExampleAssets : IStoreAssets 											//Extend from IStoreAssets (required to define assets)
	{
		public int GetVersion() {														// Get Current Version
			return 0;
		}
		
		public VirtualCurrency[] GetCurrencies() {										// Get/Setup Virtual Currencies
			return new VirtualCurrency[]{};
		}

		public VirtualGood[] GetGoods() {												// Add "TURN_GREEN" IAP to GetGoods
			return new VirtualGood[]{TURN_GREEN};
		}
		
		public VirtualCurrencyPack[] GetCurrencyPacks() {								// Get/Setup Currency Packs
			return new VirtualCurrencyPack[]{};
		}
		
		public VirtualCategory[] GetCategories() {										// Get/ Setup Categories (for In App Purchases)
			return new VirtualCategory[]{};
		}

		//****************************BOILERPLATE ABOVE(modify as you see fit/ if nessisary)***********************
		public const string TURN_GREEN_PRODUCT_ID = "com.appsflyer.product1";				//create a string to store the "turn green" in app purchase
		
		
		/** Lifetime Virtual Goods (aka - lasts forever **/

		// Create the 'TURN_GREEN' LifetimeVG In-App Purchase
		public static VirtualGood TURN_GREEN = new LifetimeVG(		
	    "turn_green",																// Name of IAP
	    "This will turn the cube green.",											// Description of IAP
	    "turn_green_item_id",														// Item ID (different from 'product id" used by itunes, this is used by soomla)
	    
	    // 1. assign the purchase type of the IAP (purchaseWithMarket == item cost real money),
	    // 2. assign the IAP as a market item (using its ID)
	    // 3. set the item to be a non-consumable purchase type
	    
	    //			1.					2.						3.
		new PurchaseWithMarket(TURN_GREEN_PRODUCT_ID, 0.99)
	    );
	}
}