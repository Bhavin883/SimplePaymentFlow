import { IonButton, IonCard, IonCardContent, IonCardHeader, IonCardTitle, IonCol, IonContent, IonGrid, IonHeader, IonInput, IonItem, IonList, IonPage, IonRow, IonTitle, IonToolbar } from '@ionic/react';
import { useEffect, useState } from 'react';
import { GetReceiptAsync, ReceiptList } from '../lib/data/Payment';
import { format } from "date-fns";
import { Link } from 'react-router-dom';
import './Home.css';
const Receipt: React.FC = () => {
    const [receiptList, setReceiptList] = useState<ReceiptList[]>([]);
    const [pumpId, setPumpId] = useState<string>();

    const getReceipt = () => GetReceiptAsync(pumpId!).then(response => {
        setReceiptList(response!);
    });

    const onPumpIdChanged = (val: CustomEvent) => {
        setPumpId(val.detail.value);
    };
    useEffect(() => {
        getReceipt();
    }, []);

    return (
        <IonPage>
            <IonHeader>
                <IonToolbar>
                    <IonTitle><b>Receipts</b> | <Link to="/Home">Sites</Link></IonTitle>
                </IonToolbar>
            </IonHeader>
            <IonContent fullscreen>
                <IonCard>
                        <IonCardContent>
                            <IonGrid>
                                <IonList>
                                    <IonRow>
                                        <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'>
                                            <IonInput type="text" class="custom" placeholder='Pump Id' value={pumpId} onIonChange={onPumpIdChanged}></IonInput>
                                        </IonCol>
                                        <IonCol sizeXs='3' sizeSm='3' sizeMd='3' sizeLg='3' sizeXl='3'>
                                            <IonButton onClick={() => getReceipt()}>Search</IonButton>
                                        </IonCol>
                                    </IonRow><br />
                                    <IonRow>
                                        <IonCol><b>Receipt Id</b></IonCol>
                                        <IonCol><b>Pump Id</b></IonCol>
                                        <IonCol><b>Start</b></IonCol>
                                        <IonCol><b>End</b></IonCol>
                                        <IonCol><b></b></IonCol>
                                    </IonRow>
                                    {
                                        receiptList?.map((receipt, index) => {
                                            return (
                                                <IonRow key={receipt.id} >
                                                    <IonCol>{receipt.id}</IonCol>
                                                    <IonCol>{receipt.pumpId}</IonCol>
                                                    <IonCol>{receipt.start && format(new Date(receipt.start), "dd/MM/yyyy HH:mm:ss")}</IonCol>
                                                    <IonCol>{receipt.start && format(new Date(receipt.end), "dd/MM/yyyy HH:mm:ss")}</IonCol>
                                                    <IonCol></IonCol>
                                                </IonRow>
                                            );
                                        })
                                    }
                                </IonList>
                            </IonGrid>
                        </IonCardContent>
                </IonCard>
            </IonContent>
        </IonPage>
    );
};

export default Receipt;
