﻿using ElectronicObserver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicObserver.Observer.kcsapi.api_port {
	
	public class port : APIBase {


		public override void OnResponseReceived( dynamic data ) {

			KCDatabase db = KCDatabase.Instance;


			//api_material
			db.Material.LoadFromResponse( APIName, data.api_material );

			//api_basic
			db.Admiral.LoadFromResponse( APIName, data.api_basic );

			//api_ship
			/*/
			foreach ( var elem in data.api_ship ) {

				int id = (int)elem.api_id;

				if ( !db.Ships.ContainsKey( id ) ) {
					var a = new ShipData();
					a.LoadFromResponse( APIName, elem );
					db.Ships.Add( a );

				} else {
					db.Ships[id].LoadFromResponse( APIName, elem );
				}
			}
			/*/

			//ちょっと遅くなるかもだけど、齟齬(幽霊とかドッペルゲンガーとか)が発生しなくなる
			db.Ships.Clear();
			foreach ( var elem in data.api_ship ) {

				var a = new ShipData();
				a.LoadFromResponse( APIName, elem );
				db.Ships.Add( a );
			
			}

			//*/


			//api_ndock
			foreach ( var elem in data.api_ndock ) {

				int id = (int)elem.api_id;

				if ( !db.Docks.ContainsKey( id ) ) {
					var a = new DockData();
					a.LoadFromResponse( APIName, elem );
					db.Docks.Add( a );

				} else {
					db.Docks[id].LoadFromResponse( APIName, elem );
				}
			}

			//api_deck_port
			db.Fleet.LoadFromResponse( APIName, data.api_deck_port );
			db.Fleet.CombinedFlag = (int)data.api_combined_flag;			//fixme:きたない
			
			
			base.OnResponseReceived( (object)data );
		}

		public override string APIName {
			get { return "api_port/port"; }
		}
	}

}