import { IonButton, IonButtons, IonCard, IonCardContent, IonCardHeader, IonCardSubtitle, IonCardTitle, IonCol, IonContent, IonGrid, IonHeader, IonInput, IonList, IonModal, IonPage, IonRow, IonTitle, IonToolbar } from '@ionic/react';
import './Home.css';
import { useEffect, useState } from 'react';
import { GetReceiptAsync, GetSiteListAsync, LockPumpAsync, Pump, ReceiptList, SiteList, UnlockPumpAsync } from '../lib/data/Payment';
import { Link } from 'react-router-dom';
import { format } from "date-fns";
const Home: React.FC = () => {
  const [siteList, setSiteList] = useState<SiteList[]>([]);
  const [siteName, setSiteName] = useState<string>();
  const [pump, setPump] = useState<Pump>();
  const [isOpen, setIsOpen] = useState(false);
  const [receipt, setReceipt] = useState<ReceiptList>();
  const getSiteList = () => GetSiteListAsync(siteName!).then(response => {
    setSiteList(response!);
  });

  const onSiteNameChanged = (val: CustomEvent) => {
    setSiteName(val.detail.value);
  };
  const onClickUnlock = (val: any) => {
    UnlockPumpAsync(val).then(response => {
      getSiteList();
    })
  };
  const onClickStop = (val: any) => {
    LockPumpAsync(val).then(response => {
      getSiteList();
    })
  };
  const onClickReceipt = (val: any) => {

    // setPump(val);
    GetReceiptAsync(val).then(response => {
      setReceipt(response![0]);
      setIsOpen(true);
    });
  };
  useEffect(() => {
    getSiteList();

  }, []);
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle><b>Sites</b> | <Link to="/Receipt">Receipts</Link></IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonCard>
          <IonCardContent>
            <IonGrid>
              <IonList>
                <IonRow>
                  <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'><IonInput type="text" class="custom" placeholder='Site Name' value={siteName} onIonChange={onSiteNameChanged}></IonInput></IonCol>
                  <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'><IonButton onClick={() => getSiteList()}>Search</IonButton></IonCol>
                </IonRow> <br />
                <IonRow>
                  <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'><b>Site</b></IonCol>
                  <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'><b>Pump</b></IonCol>
                </IonRow>
                {
                  siteList?.map((site, index) => {
                    return (
                      <IonRow key={site.id}>
                        <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'>{site.name}</IonCol>
                        <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'>
                          {
                            site.pumps?.map((pump, index) => {
                              return (
                                <IonRow key={pump.id}>
                                  <IonCol>{pump.name}</IonCol>
                                  <IonCol>{
                                    pump.locked ? <IonButton size='small' fill="outline" color='success' onClick={() => onClickUnlock(pump.id)}>Unlock</IonButton>
                                      : <IonButton size='small' fill="outline" onClick={() => onClickStop(pump.id)} color='danger'>Stop</IonButton>}
                                  </IonCol>
                                  <IonCol>{pump.locked ? <IonButton size='small' fill="outline" color='success' onClick={() => onClickReceipt(pump.id)}>Receipt</IonButton>
                                    : ''}
                                  </IonCol>
                                  <IonCol></IonCol>
                                </IonRow>
                              );
                            })
                          }
                        </IonCol>
                      </IonRow>
                    );
                  })
                }
              </IonList>
            </IonGrid>
          </IonCardContent>
        </IonCard>
      </IonContent>

      <IonModal handle={false} backdropDismiss={false} swipeToClose={true} isOpen={isOpen} >
        <IonContent>
          <IonButtons>
            <IonButton onClick={() => setIsOpen(false)}>Close</IonButton>
          </IonButtons>

          <IonCard>
            <IonCardHeader>
              <IonCardTitle><h2><b>Receipt</b></h2></IonCardTitle>
            </IonCardHeader>
            <IonCardContent>
              <br />
              <IonRow>
                <IonCol>Receipt Id</IonCol>
                <IonCol>{receipt && receipt!.id}</IonCol>
              </IonRow>
              <IonRow>
                <IonCol>Pump Id</IonCol>
                <IonCol>{receipt && receipt!.pumpId}</IonCol>
              </IonRow>
              <IonRow>
                <IonCol>Start Time</IonCol>
                <IonCol>{receipt && format(new Date(receipt!.start), "dd/MM/yyyy HH:mm:ss")}</IonCol>
              </IonRow>
              <IonRow>
                <IonCol>End Time</IonCol>
                <IonCol>{receipt && format(new Date(receipt!.end), "dd/MM/yyyy HH:mm:ss")}</IonCol>
              </IonRow>
            </IonCardContent>
          </IonCard>
        </IonContent>
      </IonModal>

    </IonPage>

  );
};

export default Home;

